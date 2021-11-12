// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools
{
    internal static class BrowserLanguageHelper
    {
        /// <summary>
        /// Returns the browser locales from the request.
        /// da, en-gb;q=0.8, en;q=0.7 -> list with 'da', 'en-gb' and 'en'
        /// </summary>
        public static IEnumerable<string> GetBrowserLanguages(HttpRequest request)
        {
            return (request.GetTypedHeaders().AcceptLanguage?.Select(kv => kv.Value.Value) ?? Enumerable.Empty<string>())
                .Select(CleanupUserLanguage)
                .Where(x => !string.IsNullOrWhiteSpace(x));
        }

        private static string CleanupUserLanguage(string requestUserLanguage)
        {
            return requestUserLanguage?.Split(';').FirstOrDefault()?.Trim() ?? string.Empty;
        }
    }
}
