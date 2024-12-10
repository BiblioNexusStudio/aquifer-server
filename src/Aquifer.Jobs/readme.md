# Aquifer.Jobs

We have two kinds of Azure Function jobs, MessageSubscribers and Managers.  Both kinds are triggered and run asynchronously.

## Managers

Managers run on a schedule with an Azure `TimerTrigger`.  Timer triggers do not retry by default but you can add a `FixedDelayRetryAttribute`
or an `ExponentialBackoffRetryAttribute` if desired.  Timer data is stored in Azure blob storage (you should never access it programatically).

Azure has built in logging on error and built in logging on retry (if a retry attribute is provided).  You should add logging on success.

## Subscribers

Subscribers listen to an Azure Storage Queue via a `QueueTrigger` and run when a new queue message is found.
By default these kinds of Azure Functions will retry up to five times and will log on error on retry
(these settings are configurable in the `host.json` file). You should add logging on success.

Note that queue names should be specified as `kebab-case`, should begin with a verb, and have max length limitations.

Queue processing code should assume failure.  Thus, each queue should ideally only take a single action and should successfully handle retries
if a failure occurs at any stage of execution.  If necessary, prefer to risk performing an operation twice on failure,
such as sending an email to a user, rather than not doing the operation at all.

If the queue processing fails after all retries then the queue message will be moved from the
queue (e.g. `queue-name`) to the poison queue (e.g. `queue-name-poison`) to await dev intervention.
Using [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer) you can view these messages
and move them back to the original queue for manual replay.

### Publishing Example

TODO Add example publish code links

### Subscribing Example

TODO Add example subscribe code links