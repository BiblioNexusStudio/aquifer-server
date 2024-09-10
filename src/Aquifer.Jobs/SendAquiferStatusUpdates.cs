using Aquifer.Common;
using Aquifer.Common.Clients;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Aquifer.Jobs;

public class SendAquiferStatusUpdates(AquiferDbContext _dbContext, SendGridClient _client, AzureKeyVaultClient _keyVaultClient)
{
    [Function(nameof(SendAquiferStatusUpdates))]
    public async Task Run([TimerTrigger("%AquiferStatus:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
    {
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
        var subscribers = await _dbContext.ContentSubscribers
            .Where(cs => cs.Enabled)
            .Include(cs => cs.ContentSubscriberLanguages)
            .ThenInclude(cs => cs.Language)
            .Include(cs => cs.ContentSubscriberParentResources)
            .ThenInclude(cs => cs.ParentResource)
            .Select(cs => new SubscriberInfo
        {
            Name = cs.Name,
            Email = cs.Email,
            UnsubscribeId = cs.UnsubscribeId,
            LanguagesSubscribed = cs.ContentSubscriberLanguages,
            ResourcesSubscribed = cs.ContentSubscriberParentResources
        }).ToListAsync(ct);

        var htmlTemplate = _dbContext.EmailTemplates
            .Single(t => t.Id == (int)EmailTemplate.AquiferMarketingNotification);

        foreach (var subscriberInfo in subscribers)
        {
            var anythingSubscribedUpdated = false;
            var resourcesLanguages = "";
            foreach (var languageEntity in subscriberInfo.LanguagesSubscribed)
            {
                foreach (var parentResourceEntity in subscriberInfo.ResourcesSubscribed)
                {
                    if (!anythingSubscribedUpdated)
                    {
                        anythingSubscribedUpdated = _dbContext.ResourceContentVersions
                            .Where(rcv => rcv.IsPublished && rcv.ResourceContent.LanguageId == languageEntity.LanguageId &&
                                          rcv.ResourceContent.Resource.ParentResourceId == parentResourceEntity.ParentResourceId)
                            .Any(rcv => rcv.ResourceContent.Resource.ResourceContents.Max(rc => rc.Updated) >= thirtyDaysAgo);
                    }

                    resourcesLanguages += $"{parentResourceEntity.ParentResource.DisplayName} - {languageEntity.Language.DisplayName}\n";
                }
            }

            if (!anythingSubscribedUpdated)
            {
                continue;
            }

            var htmlContent = htmlTemplate.Template
                .Replace("[NAME]", subscriberInfo.Name)
                .Replace("[RESOURCES]", resourcesLanguages)
                .Replace("[RESOURCE_LINK]", "https://www.aquifer.bible/aquifer-resources")
                .Replace("[UNSUBSCRIBE]", $"https://qa.admin.aquifer.bible/marketing/unsubscribe/{subscriberInfo.UnsubscribeId}?api-key=none");
            await _client.SendEmail(new SendGridEmailConfiguration
            {
                FromEmail = "no-reply@aquifer.bible",
                FromName = "Aquifer",
                Subject = htmlTemplate.Subject,
                ToAddresses =
                [
                    new EmailAddress(subscriberInfo.Email, subscriberInfo.Name)
                ],
                HtmlContent = htmlContent
            }, ct);

        }
    }
    private class SubscriberInfo
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string UnsubscribeId { get; set; }
        public required ICollection<ContentSubscriberLanguageEntity> LanguagesSubscribed { get; set; }
        public required ICollection<ContentSubscriberParentResourceEntity> ResourcesSubscribed { get; set; }
    }
}