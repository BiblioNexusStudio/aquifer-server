﻿using System.Net;
using System.Reflection;
using Aquifer.Common.Messages;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Http;

public class HealthTrigger(IQueueClientFactory _queueClientFactory, ILogger<HealthTrigger> _logger)
{
    [Function("Health")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        CancellationToken ct)
    {
        var queueNames = typeof(Queues).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(qn => qn is { IsLiteral: true, IsInitOnly: false } && qn.FieldType == typeof(string))
            .Select(qn => (string)qn.GetRawConstantValue()!)
            .ToList();

        var isHealthy = true;
        foreach (var queue in queueNames)
        {
            var client = await _queueClientFactory.GetQueueClientAsync(queue, ct);
            var topMessage = await client.PeekMessageAsync(ct);
            if (topMessage.Value?.InsertedOn?.UtcDateTime <= DateTime.UtcNow.AddHours(-1))
            {
                isHealthy = false;
                _logger.LogError("Queue {queue} is not being drained.", queue);
            }
        }

        var response = req.CreateResponse(isHealthy ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync(isHealthy ? "Healthy" : "Unhealthy", ct);

        return response;
    }
}