using Configuration_And_HttpClient.ServiceContracts;
using System.Text.Json;

namespace Configuration_And_HttpClient.Services
{
    public class FinnHubService : IFinnHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FinnHubService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // From Interface
        public async Task<Dictionary<string, object>> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                string? ur = "https://finnhub.io/api/v1/quote";
                // 1 - Prepare
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{ur}?symbol={stockSymbol}&token=cq3a8s1r01qobiisg640cq3a8s1r01qobiisg64g"),
                    Method = HttpMethod.Get
                };

                // 2 - Launch
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // 3
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);

                // 4
                string response = streamReader.ReadToEnd();

                // Serialize the json String
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from FinnHub");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }

                return responseDictionary;
            };
        }
    }
}
