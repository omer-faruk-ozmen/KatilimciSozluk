using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.Entry;
using KatilimciSozluk.Common.Infrastructure;

namespace KatilimciSozluk.Projections.VoreService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connectionString = _configuration.GetConnectionString("SqlServer");

        var voteService = new VoteService.Services.VoteService(connectionString);

        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(KatilimciSozlukConstants.VoteExchangeName)
            .EnsureQueue(KatilimciSozlukConstants.CreateEntryVoteQueueName, KatilimciSozlukConstants.VoteExchangeName)
            .Receive<CreateEntryVoteEvent>(vote =>
            {
                voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation("Create Entry Received EntryId: {0}, VoteType: {1}", vote.EntryId, vote.VoteType);
            })
            .StartConsuming(KatilimciSozlukConstants.CreateEntryVoteQueueName);
    }
}