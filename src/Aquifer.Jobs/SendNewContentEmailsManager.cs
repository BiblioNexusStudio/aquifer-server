using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Jobs.Common;
using Aquifer.Jobs.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Aquifer.Jobs;

public class SendNewContentEmailsManager(
    AquiferDbContext dbContext,
    IEmailMessagePublisher emailMessagePublisher,
    IOptions<ConfigurationOptions> options,
    TelemetryClient telemetryClient)
{
    [Function(nameof(SendNewContentEmailsManager))]
#pragma warning disable IDE0060 // Remove unused parameter: A (non-discard) TimerInfo parameter is required for correct Azure bindings
    public async Task Run([TimerTrigger(CronSchedules.FirstOfMonthAtNoon)] TimerInfo timerInfo, CancellationToken ct)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        var subscribers = await GetSubscribersAsync(ct);
        var allNewItems = await GetAllNewItemsAsync(subscribers, ct);

        if (allNewItems.Count == 0)
        {
            return;
        }

        await SendNewContentEmailsAsync(subscribers, allNewItems, ct);
    }

    private async Task SendNewContentEmailsAsync(List<SubscriberInfo> subscribers,
        List<UpdatedParentResources> allNewItems,
        CancellationToken ct)
    {
        var emailTemplate =
            await dbContext.EmailTemplates.SingleAsync(t => t.Id == (int)EmailTemplateType.MarketingNewContentNotification, ct);

        foreach (var subscriber in subscribers)
        {
            var subscriberLanguageIds = subscriber.Languages.Select(l => l.Id);
            var subscriberParentResourceIds = subscriber.ParentResources.Select(pr => pr.Id);

            var newItems = allNewItems.Where(x =>
                    subscriberLanguageIds.Contains(x.LanguageId) && subscriberParentResourceIds.Contains(x.ParentResourceId))
                .ToList();

            if (newItems.Count == 0)
            {
                continue;
            }

            var emailContent = BuildEmailContent(newItems, emailTemplate, subscriber);
            await SendEmailAsync(emailTemplate, subscriber, emailContent, ct);

            TrackSendEvent(subscriber, emailContent);
        }
    }

    private void TrackSendEvent(SubscriberInfo subscriber, string emailContent)
    {
        telemetryClient.TrackEvent("marketing-new-content-email-sent",
            new Dictionary<string, string>
            {
                { "email", subscriber.Email },
                { "name", subscriber.Name },
                { "content", emailContent }
            });
    }

    private async Task SendEmailAsync(EmailTemplateEntity emailTemplate,
        SubscriberInfo subscriber,
        string emailContent,
        CancellationToken ct)
    {
        await emailMessagePublisher.PublishSendEmailMessageAsync(
            new SendEmailMessage(
                From: new EmailAddress(options.Value.MarketingEmail.Address, options.Value.MarketingEmail.Name),
                Subject: emailTemplate.Subject,
                HtmlContent: emailContent,
                Tos: [new EmailAddress(subscriber.Email, subscriber.Name)]),
            ct);
    }

    private string BuildEmailContent(List<UpdatedParentResources> newItems, EmailTemplateEntity emailTemplate, SubscriberInfo subscriber)
    {
        var resourcesLanguages = newItems.Aggregate("",
            (current, item) => current + $"{item.ParentResourceDisplayName} - {item.LanguageEnglishDisplayName}<br />");

        return emailTemplate.Template.Replace("[NAME]", subscriber.Name)
            .Replace("[RESOURCES]", resourcesLanguages)
            .Replace("[RESOURCE_LINK]", options.Value.MarketingEmail.ResourceLink)
            .Replace("[UNSUBSCRIBE]", $"{options.Value.AquiferApiBaseUri}/marketing/unsubscribe/{subscriber.UnsubscribeId}?api-key=none");
    }

    private async Task<List<UpdatedParentResources>> GetAllNewItemsAsync(List<SubscriberInfo> subscribers, CancellationToken ct)
    {
        var today = DateTime.Today;
        var startOfThisMonth = new DateTime(today.Year, today.Month, 1);
        var startOfLastMonth = startOfThisMonth.AddMonths(-1);

        var allLanguageIds = subscribers.SelectMany(x => x.Languages.Select(l => l.Id));
        var allParentResourceIds = subscribers.SelectMany(x => x.ParentResources.Select(pr => pr.Id));

        return await dbContext.ResourceContentVersions
            .Where(x => x.IsPublished &&
                x.Updated >= startOfLastMonth &&
                x.Updated < startOfThisMonth &&
                allLanguageIds.Contains(x.ResourceContent.LanguageId) &&
                allParentResourceIds.Contains(x.ResourceContent.Resource.ParentResourceId))
            .Select(x => new UpdatedParentResources
            {
                ParentResourceId = x.ResourceContent.Resource.ParentResourceId,
                ParentResourceDisplayName = x.ResourceContent.Resource.ParentResource.DisplayName,
                LanguageId = x.ResourceContent.LanguageId,
                LanguageEnglishDisplayName = x.ResourceContent.Language.EnglishDisplay
            })
            .Distinct()
            .ToListAsync(ct);
    }

    private async Task<List<SubscriberInfo>> GetSubscribersAsync(CancellationToken ct)
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
        public required int ParentResourceId { get; init; }
        public required string ParentResourceDisplayName { get; init; }
        public required int LanguageId { get; init; }
        public required string LanguageEnglishDisplayName { get; init; }
    }

    private class SubscriberInfo
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string UnsubscribeId { get; init; }
        public required IEnumerable<LanguageEntity> Languages { get; init; }
        public required IEnumerable<ParentResourceEntity> ParentResources { get; init; }
    }
}
