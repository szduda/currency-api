using System.Threading;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace currency_api
{
  public class CurrencyManager : ICurrencyManager
  {
    private IEnumerable<CurrencyInfo> _data = Enumerable.Empty<CurrencyInfo>();
    private readonly CurrencyProvider _currencyProvider;

    public CurrencyManager(CurrencyProvider currencyProvider)
    {
      _currencyProvider = currencyProvider;
    }

    public async Task<IEnumerable<CurrencyInfo>> GetData()
    {
      // todo: read from db and set to Data prop
      _data = new CurrencyInfo[]
      {
        new CurrencyInfo{
          Date = new DateTime(2020, 01, 31),
          Code = "USD",
          Rate = 3.15M
        },
        new CurrencyInfo {
          Date = new DateTime(2020, 01, 30),
          Code = "USD",
          Rate = 3.20M
        },
        new CurrencyInfo {
          Date = new DateTime(2020, 01, 30),
          Code = "GBP",
          Rate = 4.90M
        }
      };

      var token = new CancellationToken(false);
      await _currencyProvider.FetchCurrency(token);
      return _data;
    }

    public async Task<CurrencyInfo> FetchPoint(
      DateTime date,
      string currency
      )
    {
      var token = new CancellationToken(false);
      await _currencyProvider.FetchCurrency(date, currency, token);
      // todo: save to db
      return _currencyProvider.Currency;
    }
  }
}