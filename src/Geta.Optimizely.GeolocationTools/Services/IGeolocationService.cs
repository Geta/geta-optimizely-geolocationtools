// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Net;
using EPiServer.DataAbstraction;
using EPiServer.Personalization;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools.Services
{
    public interface IGeolocationService
    {
        /// <summary>
        /// Gets the language based on the users' location and their browser preferences, depending on what is available.
        /// 1. Language branch for both the users' country and their browser preferences
        /// 2. Language branch for users' browser preferences
        /// 3. Fallback language
        /// </summary>
        LanguageBranch GetLanguage(HttpRequest requestBase);

        /// <summary>
        /// Gets the language based on the users' location.
        /// 1. Language branch for the users country
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByCountry(IGeolocationResult location);

        /// <summary>
        /// Gets the language based on the users' location.
        /// 1. Language branch for the users country
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByCountry(HttpRequest requestBase);

        /// <summary>
        /// Gets the language based on the users' browser preferences.
        /// 1. Language branch for users' browser preferences
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByBrowserPreferences(IEnumerable<string> userBrowserLanguages);

        /// <summary>
        /// Gets the language based on the users' browser preferences.
        /// 1. Language branch for users' browser preferences
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByBrowserPreferences(HttpRequest requestBase);

        /// <summary>
        /// Gets the language based on the users' location AND browser preferences.
        /// 1. Language branch for users' location AND browser preferences
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByCountryAndBrowserLanguage(
            IGeolocationResult location,
            IEnumerable<string> userBrowserLanguages);

        /// <summary>
        /// Gets the language based on the users' location AND browser preferences.
        /// 1. Language branch for users' location AND browser preferences
        /// 2. null
        /// </summary>
        LanguageBranch GetLanguageByCountryAndBrowserLanguage(HttpRequest requestBase);

        IGeolocationResult GetLocation(HttpRequest requestRequest);

        IGeolocationResult GetLocation(IPAddress ipAddress);

        /// <summary>
        /// Returns the browser locales from the request.
        /// da, en-gb;q=0.8, en;q=0.7 -> list with 'da', 'en-gb' and 'en'
        /// </summary>
        IEnumerable<string> GetBrowserLanguages(HttpRequest request);
    }
}
