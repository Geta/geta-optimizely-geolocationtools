// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Linq;
using Geta.Optimizely.GeolocationTools.Commerce.Services;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools.Commerce
{
    public class CurrentMarketFromGeolocation : ICurrentMarket
    {
        private const string MarketCookie = "MarketFromGeolocation";
        protected readonly MarketId DefaultMarketId;
        protected readonly IMarketService MarketService;
        protected readonly ICommerceGeolocationService CommerceGeolocationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public CurrentMarketFromGeolocation(
            IMarketService marketService,
            ICommerceGeolocationService commerceGeolocationService,
            IHttpContextAccessor httpContextAccessor)
        {
            MarketService = marketService;
            DefaultMarketId = marketService.GetAllMarkets().FirstOrDefault(x => x.IsEnabled)?.MarketId ?? new MarketId("DEFAULT");
            CommerceGeolocationService = commerceGeolocationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual IMarket GetCurrentMarket()
        {
            var marketId = GetMarketId();
            return GetMarket(marketId);
        }

        private MarketId GetMarketId()
        {
            var marketName = GetCookieValue();
            if (!string.IsNullOrEmpty(marketName))
            {
                return new MarketId(marketName);
            }

            if (_httpContextAccessor.HttpContext == null)
            {
                return DefaultMarketId;
            }

            var result = CommerceGeolocationService.GetMarket(_httpContextAccessor.HttpContext.Request);
            var market = result.Market;
            var marketId = market?.MarketId ?? DefaultMarketId;
            SetCookie(marketId.Value);

            return marketId;
        }

        public virtual void SetCurrentMarket(MarketId marketId)
        {
            SetCookie(marketId.Value);
        }

        protected virtual IMarket GetMarket(MarketId marketId)
        {
            return MarketService.GetMarket(marketId) ?? MarketService.GetMarket(DefaultMarketId);
        }

        private string GetCookieValue()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[MarketCookie];
        }

        private void SetCookie(string value)
        {
            HttpContext?.Response.Cookies.Append(MarketCookie, value);
        }
    }
}
