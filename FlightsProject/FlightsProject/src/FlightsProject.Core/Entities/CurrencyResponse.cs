using Newtonsoft.Json;

namespace FlightsProject.Core.Entities;

public class CurrencyResponse
{
  [JsonProperty("data")]
  public IDictionary<string, CurrencyDetails> Data { get; set; }

  public CurrencyResponse()
  {
    Data = new Dictionary<string, CurrencyDetails>();
  }
}
