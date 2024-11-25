using Newtonsoft.Json;

namespace FlightsProject.Core.Entities;
public class CurrencyRatesResponse
{
  [JsonProperty("data")]
  public IDictionary<string, double> Data { get; set; }

  public CurrencyRatesResponse()
  {
    Data = new Dictionary<string, double>();
  }
}
