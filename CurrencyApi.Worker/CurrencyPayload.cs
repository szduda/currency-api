using System.Collections.Generic;

namespace currency_api
{
  public class CurrencyRate
  {
    public string EffectiveDate { get; set; }
    public decimal Mid { get; set; }
  }

  public class CurrencyPayload
  {
    public string Code { get; set; }
    public IEnumerable<CurrencyRate> Rates { get; set; }

  }
}