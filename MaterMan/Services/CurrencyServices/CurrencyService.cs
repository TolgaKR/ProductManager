using MaterMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private const string TCMB_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Currency>> GetCurrenciesAsync()
    {
        var response = await _httpClient.GetStringAsync(TCMB_URL);
        var xml = XDocument.Parse(response);

        var currencies = xml.Descendants("Currency")
            .Select(x => new Currency
            {
                Name = x.Element("CurrencyName")?.Value,
                Code = x.Attribute("CurrencyCode")?.Value,
                ForexBuying = decimal.TryParse(x.Element("ForexBuying")?.Value, out var buying) ? buying : 0,
                ForexSelling = decimal.TryParse(x.Element("ForexSelling")?.Value, out var selling) ? selling : 0
            }).ToList();

        return currencies;
    }
}
