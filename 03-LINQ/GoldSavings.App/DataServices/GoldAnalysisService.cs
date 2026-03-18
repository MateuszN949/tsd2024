using System;
using System.Collections.Generic;
using System.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public double GetAveragePrice()
        {
            return _goldPrices.Average(p => p.Price);
        }

        public List<(int, double)> GetAveragePriceFromYears(IEnumerable<int> years)
        {
            return [..
                from goldPrice in _goldPrices
                where years.Contains(goldPrice.Date.Year)
                group goldPrice by goldPrice.Date.Year into gr
                select (gr.Key, gr.Average(p => p.Price))];
        }

        public List<GoldPrice> GetTopPricesMethod(int num, bool highest = true)
        {
            return GetTopPricesMethod(new Range(0, num), highest);
        }
        public List<GoldPrice> GetTopPricesMethod(Range range, bool highest = true)
        {
            return [.. _goldPrices.OrderBy(gp => highest ? -gp.Price : gp.Price).Take(range)];
        }

        public List<GoldPrice> GetTopPricesQuery(int num, bool highest = true)
        {
            return GetTopPricesQuery(new Range(0, num), highest);
        }
        public List<GoldPrice> GetTopPricesQuery(Range range, bool highest = true)
        {
            IEnumerable<GoldPrice> goldPrices =
                from goldprice in _goldPrices
                orderby (highest ? -goldprice.Price : goldprice.Price)
                select goldprice;

            return [.. goldPrices.Take(range)];
        }

        public List<(GoldPrice, List<GoldPrice>)> FindBenefitableDates(double returnRatio)
        {
            return [..
                from buyDate in _goldPrices
                let sellDates =
                    (from sellDate in _goldPrices
                    where sellDate.Date > buyDate.Date && sellDate.Price > returnRatio * buyDate.Price
                    select sellDate).ToList()
                where sellDates.Count != 0
                select (buyDate, sellDates)];
        }

        public (GoldPrice, GoldPrice, double) FindBestInvestment()
        {
            return (from buyDate in _goldPrices
            let bestSell =
                (from sellDate in _goldPrices
                where sellDate.Date > buyDate.Date
                select sellDate).MaxBy(sd => sd.Price)
            where bestSell != null
            select (buyDate, bestSell, (bestSell.Price - buyDate.Price) / buyDate.Price)).OrderByDescending(i => i.Item3).FirstOrDefault();
        }
    }
}
