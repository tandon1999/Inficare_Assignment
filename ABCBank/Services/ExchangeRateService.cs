using Newtonsoft.Json;

namespace ABCBank.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRateResponse> GetExchangeRateAsync(string fromDate, string toDate)
        {
            var url = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={fromDate}&to={toDate}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<ExchangeRateResponse>(response);
        }
    }
    public class ExchangeRateResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public List<Payload> Payload { get; set; }
    }

    public class Payload
    {
        public string Date { get; set; }
        public string PublishedOn { get; set; }
        public string ModifiedOn { get; set; }
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        public Currency Currency { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
    }

    public class Currency
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
    }
}
