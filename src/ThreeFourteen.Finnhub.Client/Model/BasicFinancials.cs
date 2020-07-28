using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class BasicFinancials
    {
        [JsonProperty("metric")]
        public Metric Metric { get; set; }

        [JsonProperty("metricType")]
        public MetricType MetricType { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }

    public class Metric
    {
        [JsonProperty("10DayAverageTradingVolume")]
        public double TenDayAverageTradingVolume { get; set; }

        [JsonProperty("13WeekPriceReturnDaily")]
        public double ThirteenWeekPriceReturnDaily { get; set; }

        [JsonProperty("26WeekPriceReturnDaily")]
        public double TwentySixWeekPriceRetuenDaily { get; set; }

        [JsonProperty("3MonthAverageTradingVolume")]
        public double ThreeMonthAverageTradingVolume { get; set; }

        [JsonProperty("52WeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        [JsonProperty("52WeekHighDate")]
        public DateTime FiftyTwoWeekHighDate { get; set; }

        [JsonProperty("52WeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        [JsonProperty("52WeekLowDate")]
        public DateTime FiftyTwoWeekLowDate { get; set; }

        [JsonProperty("52WeekPriceReturnDaily")]
        public double FiftyTwoWeekPriceReturnDaily { get; set; }

        [JsonProperty("5DayPriceReturnDaily")]
        public double FiveDayPriceReturnDaily { get; set; }

        [JsonProperty("beta")]
        public double beta { get; set; }

        [JsonProperty("marketCapitalization")]
        public double MarketCapitalization { get; set; }
    }
}
