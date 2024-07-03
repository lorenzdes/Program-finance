using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace Program_finance
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Fetch historical prices
            var historicalPrices = await FetchHistoricalPrices("AAPL");

            // Create an instance of Finance for the latest price
            if (historicalPrices.Any())
            {
                var latestPrice = historicalPrices.First();
                Finance apple = new Finance(1, "Apple", (float)latestPrice.Close);

                // Output the properties of the apple instance
                Console.WriteLine($"ID: {apple.ID}, Stock: {apple.Stock}, Price: {apple.Price}");
            }
            else
            {
                Console.WriteLine("No historical data found for the specified period.");
            }
        }

        static async Task<IEnumerable<Candle>> FetchHistoricalPrices(string ticker)
        {
            try
            {
                DateTime endDate = DateTime.UtcNow;
                DateTime startDate = endDate.AddDays(-10);

                // Fetch historical prices
                var historicalPrices = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, Period.Daily);

                // Filter the data to only include the last 10 days
                var filteredPrices = historicalPrices
                    .Where(candle => candle.DateTime >= startDate && candle.DateTime <= endDate)
                    .OrderByDescending(candle => candle.DateTime);

                return filteredPrices;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching historical prices: {ex.Message}");
                return Enumerable.Empty<Candle>();
            }
        }
    }
}
