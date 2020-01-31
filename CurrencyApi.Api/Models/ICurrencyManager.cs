using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace currency_api
{
  public interface ICurrencyManager
  {
    Task<IEnumerable<CurrencyInfo>> GetData();
    Task<CurrencyInfo> FetchPoint(DateTime date, string currency);
  }
}