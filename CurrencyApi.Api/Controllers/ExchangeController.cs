using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace currency_api
{
  [Route("[controller]/[action]")]
  public class ExchangeController : ControllerBase
  {
    private readonly ILogger<ExchangeController> _logger;
    private readonly ICurrencyManager _currencyManager;

    public ExchangeController(
      ILogger<ExchangeController> logger,
      ICurrencyManager currencyManager
      )
    {
      _logger = logger;
      _currencyManager = currencyManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      Console.WriteLine("Requested all data");
      var data = await GetFilteredDataSet();
      return Ok(data);
    }

    [HttpGet(ExchangeRoutes.DATA_POINT_GET)]
    [HttpGet(ExchangeRoutes.DATA_POINT_ONE_CURRENCY_GET)]
    public async Task<IActionResult> GetDataPoint(
      DateTime datePoint,
      string currency = null
      )
    {
      Console.WriteLine("Requested data on {0:yyyy-MM-dd}", datePoint);
      var data = await GetFilteredDataSet();
      var point = data.FirstOrDefault(c => c.Date == datePoint);
      if (point == null)
      {
        point = await _currencyManager.FetchPoint(datePoint, currency);
        if (point == null)
          return NotFound(_currencyManager.Error);
      }

      return Ok(point);
    }

    [HttpGet(ExchangeRoutes.DATA_RANGE_GET)]
    [HttpGet(ExchangeRoutes.DATA_RANGE_ONE_CURRENCY_GET)]
    public async Task<IActionResult> GetDataRange(
      DateTime startDate,
      DateTime endDate,
      string currency
      )
    {
      Console.WriteLine("Requested data from {0} to {1} ", startDate, endDate);

      var data = await GetFilteredDataSet(currency);
      data = data.Where(c =>
        c.Date >= startDate
        && c.Date <= endDate
      );

      return Ok(data);

    }

    [HttpGet(ExchangeRoutes.DATA_RANGE_ONE_CURRENCY_AVG_GET)]
    public async Task<IActionResult> GetDataRangeAverageRate(
      DateTime startDate,
      DateTime endDate,
      string currency = null
      )
    {
      Console.WriteLine("Requested average rate of data from {0} to {1}", startDate, endDate);

      var data = await GetFilteredDataSet(currency);
      var avg = data.Where(c =>
        c.Date >= startDate
        && c.Date <= endDate)
        .Average(c => c.Rate);

      return Ok(avg);
    }

    private async Task<IEnumerable<CurrencyInfo>> GetFilteredDataSet(string currency = null)
    {
      var data = await _currencyManager.GetData();

      if (!string.IsNullOrEmpty(currency))
      {
        Console.WriteLine("Filtering by currency {0}", currency);
        data = data.Where(c => c.Code.ToLower() == currency.ToLower());
      }

      return data;
    }
  }
}
