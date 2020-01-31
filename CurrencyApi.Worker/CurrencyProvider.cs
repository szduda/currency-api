using System.Linq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace currency_api
{
  public class CurrencyProvider
  {
    private const string GetCurrencyUri =
            "http://api.nbp.pl/api/exchangerates/rates/a/usd/";
    private readonly HttpClient _httpClient;

    public CurrencyProvider()
    {
      _httpClient = new HttpClient();
    }

    public CurrencyInfo Currency { get; private set; } = null;

    public async Task FetchCurrency(CancellationToken cancellationToken)
    {
      try
      {
        var response = await _httpClient.GetAsync(GetCurrencyUri, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
          var responseString = await response.Content.ReadAsStringAsync();
          var payload = JsonConvert
          .DeserializeObject<CurrencyPayload>(responseString);
          var rate = payload.Rates?.FirstOrDefault();
          DateTime.TryParse(rate?.EffectiveDate, out var date);
          Currency = new CurrencyInfo
          {
            Code = payload.Code,
            Date = date,
            Rate = rate?.Mid ?? 0M
          };
        }
        SaveCurrency();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    private void SaveCurrency()
    {
      // await save to db
    }
  }
}