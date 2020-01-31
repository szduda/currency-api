using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace currency_api
{
  public class DataRefreshService : BackgroundService
  {
    private readonly ILogger<DataRefreshService> _logger; private readonly CurrencyProvider _currencyProvider;

    public DataRefreshService(
      CurrencyProvider currencyProvider,
      ILogger<DataRefreshService> logger
      )
    {
      _currencyProvider = currencyProvider;
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        await _currencyProvider.FetchCurrency(cancellationToken);
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
      }
    }
  }
}
