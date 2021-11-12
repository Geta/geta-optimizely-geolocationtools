// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Globalization;
using EPiServer.Personalization;
using Mediachase.Commerce;

namespace Geta.Optimizely.GeolocationTools.Commerce.Models
{
    public class CommerceGeolocationResult : ICommerceGeolocationResult
    {
        public IMarket Market { get; set; }
        public CultureInfo Language { get; set; }
        public IGeolocationResult Location { get; set; }
    }
}
