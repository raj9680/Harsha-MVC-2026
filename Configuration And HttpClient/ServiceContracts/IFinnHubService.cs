namespace Configuration_And_HttpClient.ServiceContracts
{
    public interface IFinnHubService
    {
        Task<Dictionary<string, object>> GetStockPriceQuote(string stockSymbol);
    }
}
