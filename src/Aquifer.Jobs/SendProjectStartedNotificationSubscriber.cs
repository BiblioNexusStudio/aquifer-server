using Aquifer.Common.Jobs;
using Aquifer.Common.Jobs.Messages;
using Aquifer.Common.Services;
using Aquifer.Data;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs;

public sealed class SendProjectStartedNotificationSubscriber(
    AquiferDbContext _dbContext,
    IEmailQueueService _emailQueueService,
    ILogger<SendProjectStartedNotificationSubscriber> _logger)
{
    [Function(nameof(SendProjectStartedNotificationSubscriber))]
    public async Task RunAsync(
        [QueueTrigger(Queues.SendProjectStartedNotification)] QueueMessage message,
        CancellationToken ct)
    {
        var projectStartedNotificationMessage = message.Deserialize<SendProjectStartedNotificationMessage, SendProjectStartedNotificationSubscriber>(_logger);

        var project = await _dbContext.Projects
            .Include(projectEntity => projectEntity.CompanyLeadUser)
            .FirstAsync(p => p.Id == projectStartedNotificationMessage.ProjectId, ct);

        var companyLead = project.CompanyLeadUser;
        if (companyLead is not null)
        {
            var templatedEmail = new TemplatedEmail(
                From: new EmailAddress("no-reply@aquifer.bible", "Aquifer"),
                Subject: "Aquifer Project Started",
                TemplateId: "", // TODO
                DynamicTemplateData: new
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                },
                Tos: [new EmailAddress(companyLead.Email, $"{companyLead.FirstName} {companyLead.LastName}")]);

            await _emailQueueService.QueueEmailAsync(templatedEmail, ct);
        }
    }
}