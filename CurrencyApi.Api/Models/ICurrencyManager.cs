using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace currency_api
{
  public interface ICurrencyManager
  {
    string Error { get; }
    Task<IEnumerable<CurrencyInfo>> GetData();
    Task<CurrencyInfo> FetchPoint(DateTime date, string currency);
  }
}