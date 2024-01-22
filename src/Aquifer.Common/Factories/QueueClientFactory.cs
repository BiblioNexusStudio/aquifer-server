using Azure.Storage.Queues;

namespace Aquifer.Common.Factories;

public interface IQueueClientFactory
{
    QueueClient GetQueueClient(string queueName);
}

public class QueueClientFactory(string? _baseName, string? _connectionString) : IQueueClientFactory
{
    private readonly Dictionary<string, QueueClient> _clients = new Dictionary<string, QueueClient>();

    public QueueClient GetQueueClient(string queueName)
    {
        var fullQueueName = $"{_baseName}-{queueName}";
        if (!_clients.ContainsKey(fullQueueName))
        {
            var client = new QueueClient(_connectionString, fullQueueName);
            _clients.Add(fullQueueName, client);
        }

        return _clients[fullQueueName];
    }
}
