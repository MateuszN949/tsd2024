using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using GoldSavings.App.Client;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldDataService
    {
        private readonly GoldClient _goldClient;

        public GoldDataService()
        {
            _goldClient = new GoldClient();
        }

        public async Task<List<GoldPrice>> GetGoldPrices(DateTime startDate, DateTime endDate)
        {
            var prices = await _goldClient.GetGoldPrices(startDate, endDate);
            return prices ?? new List<GoldPrice>();  // Prevent null values
        }

        public async Task<List<GoldPrice>> GetGoldPricesRange(DateTime from, DateTime to)
        {
            if (from > to)
            {
                (to, from) = (from, to);
            }

            List<GoldPrice> result = [];

            DateTime currentStart = from;

            while (currentStart < to)
            {
                DateTime currentEnd = currentStart.AddYears(1);

                if (currentEnd > to)
                    currentEnd = to;

                var chunk = await GetGoldPrices(currentStart, currentEnd);

                result.AddRange(chunk);

                currentStart = currentEnd.AddDays(1);
            }

            return result;
        }
    }
}
