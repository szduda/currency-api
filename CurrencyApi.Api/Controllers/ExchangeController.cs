using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace currency_api
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class ExchangeController : Controller
  {
    private readonly ILogger<ExchangeController> _logger;
    private readonly CurrencyManager _currencyManager;

    public ExchangeController(
      ILogger<ExchangeController> logger,
      CurrencyManager currencyManager
      )
    {
      _logger = logger;
      _currencyManager = currencyManager;
    }

    [HttpGet]
    public IEnumerable<CurrencyInfo> Get()
    {
      Console.WriteLine("Requested all data");
      return GetFilteredDataSet();
    }

    [HttpGet(ExchangeRoutes.DATA_POINT_GET)]
    [HttpGet(ExchangeRoutes.DATA_POINT_ONE_CURRENCY_GET)]
    public CurrencyInfo GetDataPoint(
      DateTime date,
      string currency = null
      )
    {
      Console.WriteLine("Requested data on {0}", date);
      return GetFilteredDataSet().FirstOrDefault(c => c.Date == date);
    }

    [HttpGet(ExchangeRoutes.DATA_RANGE_GET)]
    [HttpGet(ExchangeRoutes.DATA_RANGE_ONE_CURRENCY_GET)]
    public IEnumerable<CurrencyInfo> GetDataRange(
      DateTime startDate,
      DateTime endDate,
      string currency
      )
    {
      Console.WriteLine("Requested data from {0} to {1} ", startDate, endDate);

      return GetFilteredDataSet(currency)
      .Where(c =>
        c.Date >= startDate
        && c.Date <= endDate
      );

    }

    [HttpGet(ExchangeRoutes.DATA_RANGE_ONE_CURRENCY_AVG_GET)]
    public decimal GetDataRangeAverageRate(
      DateTime startDate,
      DateTime endDate,
      string currency = null
      )
    {
      var data = GetDataRange(startDate, endDate, currency);
      Console.WriteLine("Requested average");
      return data.Average(c => c.Rate);
    }

    private IEnumerable<CurrencyInfo> GetFilteredDataSet(string currency = null)
    {
      var data = _currencyManager.Data;

      if (!string.IsNullOrEmpty(currency))
      {
        Console.WriteLine("Filtering by currency {0}", currency);
        data = data.Where(c => c.Code.ToLower() == currency.ToLower());
      }

      return data;
    }
  }
}
