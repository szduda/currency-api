using System.Linq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace currency_api
{
  public class CurrencyProvider
  {

    private const string requestTemplateBase =
    "http://api.nbp.pl/api/exchangerates/rates/a/";
    private string GetCurrencyUri(DateTime date, string currency)
    {
      var uri = requestTemplateBase;
      if (!string.IsNullOrEmpty(currency))
      {
        uri += currency + "/";
      }

      var dateParam = date != DateTime.MinValue
      ? date.ToString("yyyy-MM-dd")
      : "today";
      uri += dateParam;

      Console.WriteLine(uri);
      return uri;
    }
    private readonly HttpClient _httpClient;

    public CurrencyProvider()
    {
      _httpClient = new HttpClient();
    }

    public CurrencyInfo Currency { get; private set; } = null;
    public string Error { get; private set; }

    public async Task FetchCurrency(CancellationToken cancellationToken)
    {
      await FetchCurrency(DateTime.MinValue, null, cancellationToken);
    }

    public async Task FetchCurrency(
      DateTime date,
      string currency,
      CancellationToken cancellationToken
      )
    {
      Currency = null;
      Error = null;
      try
      {
        var response = await _httpClient.GetAsync(
          GetCurrencyUri(date, currency),
          cancellationToken
          );

        if (response.IsSuccessStatusCode)
        {
          var responseString = await response.Content.ReadAsStringAsync();
          var payload = JsonConvert
          .DeserializeObject<CurrencyPayload>(responseString);
          var rate = payload.Rates?.FirstOrDefault();
          Currency = new CurrencyInfo
          {
            Code = payload.Code,
            Date = date,
            Rate = rate?.Mid ?? 0M
          };
          Console.WriteLine(Currency.Date);
          SaveCurrency();
        }
        else
        {
          Error = response.StatusCode == HttpStatusCode.NotFound
          ? "Requested data not found"
          : "An error occured during data fetch from external service";
        }
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