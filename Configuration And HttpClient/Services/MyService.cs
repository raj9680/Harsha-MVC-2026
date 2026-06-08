namespace Configuration_And_HttpClient.Services
{
    public class MyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Method()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // 1 - Prepare
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri("url"),
                    Method = HttpMethod.Get
                };

                // 2 - Launch
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            };
        }
    }
}
