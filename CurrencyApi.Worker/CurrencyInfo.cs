using System;
using System.Collections.Generic;

namespace currency_api
{
  public class CurrencyInfo
  {
    public string Code { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
  }
}