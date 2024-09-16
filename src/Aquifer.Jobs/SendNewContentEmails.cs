using Aquifer.Common;
using Aquifer.Common.Clients;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Jobs.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;

namespace Aquifer.Jobs;

public class SendNewContentEmails(
    AquiferDbContext dbContext,
    ISendGridClient client,
    IOptions<ConfigurationOptions> options,
    TelemetryClient telemetryClient,
    ILogger<SendNewContentEmails> logger)
{
    [Function(nameof(SendNewContentEmails))]
    public async Task Run([TimerTrigger("%NewContentEmail:CronSchedule%")] TimerInfo timerInfo, CancellationToken ct)
    {
        var subscribers = await GetSubscribers(ct);

        var allNewItems = await GetAllNewItems(subscribers, ct);

        if (allNewItems.Count == 0)
        {
            return;
        }

        await SendNewContentEmailsToSubscribers(subscribers, allNewItems, ct);
    }

    private async Task SendNewContentEmailsToSubscribers(List<SubscriberInfo> subscribers, List<UpdatedParentResources> allNewItems,
        CancellationToken ct)
    {
        var htmlTemplate = await dbContext.EmailTemplates.SingleAsync(t => t.Id == (int)EmailTemplate.MarketingNewContentNotification, ct);

        foreach (var subscriber in subscribers)
        {
            var subscriberLanguageIds = subscriber.Languages.Select(l => l.Id);
            var subscriberParentResourceIds = subscriber.ParentResources.Select(pr => pr.Id);

            var newItems = allNewItems.Where(x =>
                subscriberLanguageIds.Contains(x.LanguageId) && subscriberParentResourceIds.Contains(x.ParentResourceId)).ToList();

            if (newItems.Count == 0)
            {
                continue;
            }

            var resourcesLanguages =
                newItems.Aggregate("", (current, item) => current + $"{item.DisplayName} - {item.EnglishDisplay}<br />");

            var htmlContent = htmlTemplate.Template
                .Replace("[NAME]", subscriber.Name)
                .Replace("[RESOURCES]", resourcesLanguages)
                .Replace("[RESOURCE_LINK]", options.Value.MarketingEmail.ResourceLink)
                .Replace("[UNSUBSCRIBE]",
                    $"{options.Value.BaseUrl}/marketing/unsubscribe/{subscriber.UnsubscribeId}?api-key=none");

            var response = await client.SendEmailAsync(new SendGridEmailConfiguration
            {
                FromEmail = options.Value.MarketingEmail.Address,
                FromName = options.Value.MarketingEmail.Name,
                Subject = htmlTemplate.Subject,
                ToAddresses =
                [
                    new EmailAddress(subscriber.Email, subscriber.Name)
                ],
                HtmlContent = htmlContent
            }, ct);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Failed to Successfully Send Content Update Email: {response}", JsonUtilities.DefaultSerialize(response));
            }

            telemetryClient.TrackEvent("marketing-new-content-email-sent", new Dictionary<string, string>
            {
                {
                    "to-email", subscriber.Email
                },
                {
                    "to-name", subscriber.Name
                },
                {
                    "html-content", htmlContent
                }
            });
        }
    }

    private async Task<List<UpdatedParentResources>> GetAllNewItems(List<SubscriberInfo> subscribers, CancellationToken ct)
    {
        var today = DateTime.Today;
        var firstOfThisMonth = new DateTime(today.Year, today.Month, 1);
        var firstOfLastMonth = firstOfThisMonth.AddMonths(-1);

        var allLanguageIds = subscribers.SelectMany(x => x.Languages.Select(l => l.Id));
        var allParentResourceIds = subscribers.SelectMany(x => x.ParentResources.Select(pr => pr.Id));
        return await dbContext.ResourceContentVersions.Where(x =>
                x.IsPublished &&
                x.Updated >= firstOfLastMonth &&
                x.Updated < firstOfThisMonth &&
                allLanguageIds.Contains(x.ResourceContent.LanguageId) &&
                allParentResourceIds.Contains(x.ResourceContent.Resource.ParentResourceId))
            .Select(x => new UpdatedParentResources
            {
                ParentResourceId = x.ResourceContent.Resource.ParentResourceId,
                DisplayName = x.ResourceContent.Resource.ParentResource.DisplayName,
                LanguageId = x.ResourceContent.LanguageId,
                EnglishDisplay = x.ResourceContent.Language.EnglishDisplay
            })
            .ToListAsync(ct);
    }

    private async Task<List<SubscriberInfo>> GetSubscribers(CancellationToken ct)
    {
        return await dbContext.ContentSubscribers.Where(cs => cs.Enabled)
            .Select(cs => new SubscriberInfo
            {
                Name = cs.Name,
                Email = cs.Email,
                UnsubscribeId = cs.UnsubscribeId,
                Languages = cs.ContentSubscriberLanguages.Select(csl => csl.Language),
                ParentResources = cs.ContentSubscriberParentResources.Select(cspr => cspr.ParentResource)
            })
            .ToListAsync(ct);
    }

    private class UpdatedParentResources
    {
        public required int ParentResourceId { get; set; }
        public required string DisplayName { get; set; }
        public required int LanguageId { get; set; }
        public required string EnglishDisplay { get; set; }
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