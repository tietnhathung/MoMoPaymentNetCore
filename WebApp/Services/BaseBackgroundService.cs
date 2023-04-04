namespace WebApp.Services;

public class BaseBackgroundService:BackgroundService
{
    private readonly ILogger<BaseBackgroundService> _logger;

    public BaseBackgroundService(ILogger<BaseBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("BaseBackgroundService.ExecuteAsync do at {time}",DateTime.Now);
            await Task.Delay(TimeSpan.FromSeconds(10),stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BaseBackgroundService.StopAsync do at {time}",DateTime.Now);
        return base.StopAsync(cancellationToken);
    }
}