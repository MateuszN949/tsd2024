using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
using System.Linq.Expressions;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();
        DateTime endDate = DateTime.Now;
        DateTime startDate = endDate.AddDays(-183);
        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();

        // Task 1 2.a
        analysisService = new GoldAnalysisService(dataService.GetGoldPricesRange(startDate.AddYears(-1), endDate).GetAwaiter().GetResult());
        var greatestPrices = analysisService.GetTopPricesMethod(3);
        var lowestPrices = analysisService.GetTopPricesMethod(3, false);

        if (!greatestPrices.SequenceEqual(analysisService.GetTopPricesQuery(3)) || !lowestPrices.SequenceEqual(analysisService.GetTopPricesQuery(3, false)))
        {
            throw new Exception("Wrong implementation of GetTopPrices.");
        }

        // Test the timestamps
        // List<GoldPrice> dates = dataService.GetGoldPricesRange(new DateTime(2020,01,01), DateTime.Now).GetAwaiter().GetResult();

        // Task 1 2.b
        analysisService = new GoldAnalysisService(dataService.GetGoldPrices(new DateTime(2020,01,01), new DateTime(2020,01,31)).GetAwaiter().GetResult());
        var benficialDates = analysisService.FindBenefitableDates(1.05);

        // Task 1 2.c
        analysisService = new GoldAnalysisService(dataService.GetGoldPricesRange(new DateTime(2019,01,01), new DateTime(2022,12,31)).GetAwaiter().GetResult());
        var threeOpening = analysisService.GetTopPricesMethod(new Range(10, 13));

        // Task 1 2.d
        analysisService = new GoldAnalysisService(dataService.GetGoldPricesRange(new DateTime(2020,01,01), new DateTime(2024,12,31)).GetAwaiter().GetResult());
        var averages = analysisService.GetAveragePriceFromYears([2020, 2023, 2024]);

        // Task 1 2.e
        var bestInv = analysisService.FindBestInvestment();

        // Task 1 3
        GoldPrice.SaveToXml(goldPrices, "./pricesInXML");

        // Task 1 4
        var readGoldPrices = GoldPrice.LoadFromXml("./pricesInXML");
        
        if (!goldPrices.Select(g => (g.Date, g.Price)).SequenceEqual(readGoldPrices.Select(g => (g.Date, g.Price))))
        {
            throw new Exception("Wrong implementation of saving and reading gold prices.");
        }

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");
        GoldResultPrinter.PrintPrices(greatestPrices, "TOP 3 highest prices");
        GoldResultPrinter.PrintPrices(lowestPrices, "TOP 3 lowest prices");

        foreach ((GoldPrice buyDate, List<GoldPrice> sellDate) in benficialDates)
        {
            GoldResultPrinter.PrintSingleValue(buyDate.Price, $"Buy price on {buyDate.Date:yyyy-MM-dd}");
            GoldResultPrinter.PrintPrices(sellDate, "Potential sell dates to get 5% profit");
        }

        GoldResultPrinter.PrintPrices(threeOpening, "3 opening prices of the second ten in 2019-2022");

        foreach ((int year, double avg) in averages)
        {
            GoldResultPrinter.PrintSingleValue(Math.Round(avg, 2), $"Average price in year {year}");
        }

        GoldResultPrinter.PrintSingleValue(bestInv.Item1.Price, $"Price of the best day to invest ({bestInv.Item1.Date:yyyy-MM-dd})");
        GoldResultPrinter.PrintSingleValue(bestInv.Item2.Price, $"Price of the best day to sell ({bestInv.Item2.Date:yyyy-MM-dd})");
        Console.WriteLine($"\nInvestment profit: {Math.Round(bestInv.Item3 * 100, 2)}%");

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

    }
}
