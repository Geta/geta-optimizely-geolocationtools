// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Bia.Countries.Iso3166;
using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using Geta.Optimizely.GeolocationTools.Commerce.Models;
using Geta.Optimizely.GeolocationTools.Services;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools.Commerce.Services
{
    [ServiceConfiguration(ServiceType = typeof(ICommerceGeolocationService))]
    public class CommerceGeolocationService : ICommerceGeolocationService
    {
        private readonly IGeolocationService _geolocationService;
        private List<IMarket> EnabledMarkets { get; }

        public CommerceGeolocationService(IGeolocationService geolocationService, IMarketService marketService)
        {
            _geolocationService = geolocationService;
            EnabledMarkets = marketService.GetAllMarkets().Where(x => x.IsEnabled).ToList();
        }

        /// <summary>
        /// Gets the market based on the IP address location and browser UserLanguages.
        /// Defaults to market default language
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Market and language and location tuple, which can be null</returns>
        public ICommerceGeolocationResult GetMarket(HttpRequest request)
        {
            if (request == null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            var location = _geolocationService.GetLocation(request);
            if (location != null)
            {
                var (market, language) = GetMarket(request, location);
                return new CommerceGeolocationResult
                {
                    Market = market,
                    Language = language,
                    Location = location
                };
            }
            else
            {
                return new CommerceGeolocationResult();
            }
        }

        /// <summary>
        /// Gets the language based on the browser UserLanguages and the given market.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        public CultureInfo GetLanguage(HttpRequest request, IMarket market)
        {
            if (request == null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            if (market == null)
            {
                throw new System.ArgumentNullException(nameof(market));
            }

            var language = _geolocationService.GetBrowserLanguages(request)
                .Select(x => market.Languages.FirstOrDefault(l => l.Name.Equals(x)))
                .FirstOrDefault(x => x != null);
            return language;
        }

        /// <summary>
        /// Gets the market based on the IP address.
        /// </summary>
        /// <param name="geolocationResult">The geolocation result.</param>
        /// <returns></returns>
        public IMarket GetMarket(IGeolocationResult geolocationResult)
        {
            if (geolocationResult == null)
            {
                throw new System.ArgumentNullException(nameof(geolocationResult));
            }

            // Get ISO3166 country as commerce is using alpha3 codes and we only have an alpha2 code
            var country = Countries.GetCountryByAlpha2(geolocationResult.CountryCode);
            if (country == null)
            {
                return null;
            }

            // Get market with this country enabled
            var marketsWithCountry =
                EnabledMarkets.Where(market => market.Countries.Any(c => c.Equals(country.Alpha3)));

            return marketsWithCountry.FirstOrDefault();
        }

        private (IMarket market, CultureInfo uiLanguage) GetMarket(HttpRequest request, IGeolocationResult geolocationResult)
        {
            var marketWithCountry = GetMarket(geolocationResult);
            if (marketWithCountry == null)
            {
                return (null, null);
            }

            var language = GetLanguage(request, marketWithCountry);

            return (marketWithCountry, language ?? marketWithCountry.DefaultLanguage);
        }
    }
}
