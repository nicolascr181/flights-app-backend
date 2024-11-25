using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;
using freecurrencyapi;
using Newtonsoft.Json;

namespace FlightsProject.Infrastructure.Services;
public class DynamicCurrencyConverter : ICurrencyConverter
{
  private readonly Freecurrencyapi _currencyApi;

  public DynamicCurrencyConverter(string apiKey)
  {
    if (string.IsNullOrWhiteSpace(apiKey))
      throw new ArgumentException("API key must be provided", nameof(apiKey));

    _currencyApi = new Freecurrencyapi(apiKey);
  }

  public async Task<double> Convert(double amount, string targetCurrency, string baseCurrency = "USD")
  {

    if (string.IsNullOrWhiteSpace(targetCurrency))
      throw new ArgumentException("Target currency cannot be null or empty.", nameof(targetCurrency));

    if (string.IsNullOrWhiteSpace(baseCurrency))
      throw new ArgumentException("Base currency cannot be null or empty.", nameof(baseCurrency));

    if (targetCurrency.Equals("USD"))
    {
      return amount;
    }

    // Fetch the exchange rates response as a string
    string response  = await Task.FromResult(_currencyApi.Latest(baseCurrency, targetCurrency));

    // Parse the response string (assuming it returns JSON)
    var ratesResponse = JsonConvert.DeserializeObject<CurrencyRatesResponse>(response);

    if (ratesResponse == null || !ratesResponse.Data.ContainsKey(targetCurrency))
    {
      throw new ArgumentException($"Unsupported currency or data not available: {targetCurrency}");
    }

    // Perform conversion
    return amount * ratesResponse.Data[targetCurrency];
  }

  public async Task<IDictionary<string, CurrencyDetails>> GetCurrencyDetails()
  {
    // Implementation for getting all currency details
    string response = await Task.FromResult(_currencyApi.Currencies());

    CurrencyResponse? currencyResponse = JsonConvert.DeserializeObject<CurrencyResponse>(response);

    if (currencyResponse == null || (currencyResponse.Data.Count == 0))
    {
      throw new ArgumentException($"Error when getting available currencies from API");
    }


    return currencyResponse.Data;
   }

    
  }

