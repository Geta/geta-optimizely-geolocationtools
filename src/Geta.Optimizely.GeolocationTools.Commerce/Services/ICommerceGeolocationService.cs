// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Globalization;
using EPiServer.Personalization;
using Geta.Optimizely.GeolocationTools.Commerce.Models;
using Mediachase.Commerce;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools.Commerce.Services
{
    public interface ICommerceGeolocationService
    {
        /// <summary>
        /// Gets the market based on the IP address location and browser UserLanguages.
        /// Defaults to market default language. Market, language and location can be null.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Market, language and location can be null</returns>
        ICommerceGeolocationResult GetMarket(HttpRequest request);

        /// <summary>
        /// Gets the market based on the IP address.
        /// </summary>
        /// <param name="geolocationResult">The geolocation result.</param>
        /// <returns></returns>
        IMarket GetMarket(IGeolocationResult geolocationResult);

        /// <summary>
        /// Gets the language based on the browser UserLanguages and the given market.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="market">The market.</param>
        /// <returns></returns>
        CultureInfo GetLanguage(HttpRequest request, IMarket market);
    }
}
