using System;
using System.Collections.Generic;
using currency_api;

namespace CurrencyApi.Tests
{
  public class CurrencyManagerFake : ICurrencyManager
  {
    public CurrencyManagerFake()
    {
      FakeData();
    }

    public IEnumerable<CurrencyInfo> Data { get; private set; }

    private void FakeData()
    {
      Data = new CurrencyInfo[]
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
    }
  }
}