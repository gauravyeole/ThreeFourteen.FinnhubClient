﻿using System;
using System.Threading.Tasks;
using ThreeFourteen.Finnhub.Client.Model;
using ThreeFourteen.Finnhub.Client.Serialisation;

namespace ThreeFourteen.Finnhub.Client
{
    public class StockClient
    {
        private readonly FinnhubClient _finnhubClient;

        internal StockClient(FinnhubClient finnhubClient)
        {
            _finnhubClient = finnhubClient;
        }

        public Task<Company> GetCompany(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<Company> GetCompanyByIsin(string isin)
        {
            if (string.IsNullOrWhiteSpace(isin)) throw new ArgumentException(nameof(isin));

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Isin, isin));
        }

        public Task<Company> GetCompanyByCusip(string cusip)
        {
            if (string.IsNullOrWhiteSpace(cusip)) throw new ArgumentException(nameof(cusip));

            return _finnhubClient.SendAsync<Company>("stock/profile", JsonDeserialiser.Default,
                new Field(FieldKeys.Cusip, cusip));
        }

        public Task<Compensation> GetCompensation(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<Compensation>("stock/ceo-compensation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<RecommendationTrend[]> GetRecommendationTrends(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<RecommendationTrend[]>("stock/recommendation", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<PriceTarget> GetPriceTarget(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<PriceTarget>("stock/price-target", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<string[]> GetPeers(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<string[]>("stock/peers", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<Earnings[]> GetEarnings(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<Earnings[]>("stock/earnings", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public Task<StockExchange[]> GetExchanges()
        {
            return _finnhubClient.SendAsync<StockExchange[]>("stock/exchange", JsonDeserialiser.Default);
        }

        public Task<Symbol[]> GetSymbols(string exchange)
        {
            return _finnhubClient.SendAsync<Symbol[]>("stock/symbol", JsonDeserialiser.Default,
                new Field(FieldKeys.Exchange, exchange));
        }

        public Task<Quote> GetQuote(string symbol)
        {
            return _finnhubClient.SendAsync<Quote>("quote", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public async Task<NewsEntry[]> GetCompanyNews(string symbol, DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));
            if (fromDate == null || toDate == null) throw new ArgumentNullException();

            var from = fromDate.ToString("yyyy-MM-dd");
            var to = toDate.ToString("yyyy-MM-dd");

            return await _finnhubClient.SendAsync<NewsEntry[]>("/company-news", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.From, from),
                new Field(FieldKeys.To, to));
        }

        public async Task<NewsSentiment> GetNewsSentiment(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return await _finnhubClient.SendAsync<NewsSentiment>("news-sentiment", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol));
        }

        public async Task<BasicFinancials> GetBasicFinancials(string symbol, MetricType metricType)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return await _finnhubClient.SendAsync<BasicFinancials>("stock/metric", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Metric, metricType.ToString()));
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, int count)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.Count, count.ToString()))
                .ConfigureAwait(false);

            return data.Map();
        }

        public Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from)
        {
            return GetCandles(symbol, resolution, from, DateTime.UtcNow);
        }

        public async Task<Candle[]> GetCandles(string symbol, Resolution resolution, DateTime from, DateTime to)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            var data = await _finnhubClient.SendAsync<CandleData>("stock/candle", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()),
                new Field(FieldKeys.From, new DateTimeOffset(from).ToUnixTimeSeconds().ToString()),
                new Field(FieldKeys.To, new DateTimeOffset(to).ToUnixTimeSeconds().ToString()))
                .ConfigureAwait(false);

            return data.Map();
        }
    }
}
