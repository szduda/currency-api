using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace currency_api
{
  public class CurrencyManager
  {
    public CurrencyManager()
    {
      GetData();
    }

    public IEnumerable<CurrencyInfo> Data { get; private set; }

    private void GetData()
    {
      // read from db and set to Data prop
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