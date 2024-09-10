using Aquifer.Common;
using Aquifer.Common.Clients;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Aquifer.Jobs;

public class SendAquiferStatusUpdates(AquiferDbContext dbContext, SendGridClient client)
{
    [Function(nameof(SendAquiferStatusUpdates))]
    public async Task Run([TimerTrigger("%AquiferStatus:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
    {
        var today = DateTime.Today;
        var month = new DateTime(today.Year, today.Month, 1);
        var oneMonthAgo = month.AddMonths(-1);
        var subscribers = await dbContext.ContentSubscribers
            .Where(cs => cs.Enabled)
            .Select(cs => new SubscriberInfo
            {
                Name = cs.Name,
                Email = cs.Email,
                UnsubscribeId = cs.UnsubscribeId,
                Languages = cs.ContentSubscriberLanguages.Select(csl => csl.Language),
                ParentResources = cs.ContentSubscriberParentResources.Select(pr => pr.ParentResource)
            }).ToListAsync(ct);;

        var htmlTemplate = dbContext.EmailTemplates
            .Single(t => t.Id == (int)EmailTemplate.AquiferMarketingNotification);

        foreach (var subscriberInfo in subscribers)
        {
            var anythingSubscribedUpdated = false;
            var resourcesLanguages = "";
            foreach (var languageEntity in subscriberInfo.Languages)
            {
                foreach (var parentResourceEntity in subscriberInfo.ParentResources)
                {
                    if (!anythingSubscribedUpdated)
                    {
                        anythingSubscribedUpdated = dbContext.ResourceContentVersions
                            .Where(rcv => rcv.IsPublished
                                          && rcv.ResourceContent.LanguageId == languageEntity.Id
                                          && rcv.ResourceContent.Resource.ParentResourceId == parentResourceEntity.Id)
                            .Any(rcv => rcv.ResourceContent.Resource.ResourceContents.Max(rc => rc.Updated) >= oneMonthAgo);
                    }

                    resourcesLanguages += $"{parentResourceEntity.DisplayName} - {languageEntity.DisplayName}<br/>";
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
            await client.SendEmail(new SendGridEmailConfiguration
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
        public required IEnumerable<LanguageEntity> Languages { get; set; }
        public required IEnumerable<ParentResourceEntity> ParentResources { get; set; }
    }
}