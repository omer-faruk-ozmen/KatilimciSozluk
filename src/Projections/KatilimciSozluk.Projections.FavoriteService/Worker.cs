using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.Entry;
using KatilimciSozluk.Common.Infrastructure;
using KatilimciSozluk.Projections.FavoriteService.Services;

namespace KatilimciSozluk.Projections.FavoriteService;

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

        var favService = new Services.FavService(connectionString);

        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(KatilimciSozlukConstants.FavExchangeName)
            .EnsureQueue(KatilimciSozlukConstants.CreateEntryFavQueueName, KatilimciSozlukConstants.FavExchangeName)
            .Receive<CreateEntryFavEvent>(fav =>
            {
                //db insert
                favService.CreateEntryFav(fav).GetAwaiter().GetResult();

                _logger.LogInformation($"Received EntryId {fav.EntryId}");
            })
            .StartConsuming(KatilimciSozlukConstants.CreateEntryFavQueueName);
    }
}