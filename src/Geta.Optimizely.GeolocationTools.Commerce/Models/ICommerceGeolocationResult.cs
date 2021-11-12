// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Globalization;
using EPiServer.Personalization;
using Mediachase.Commerce;

namespace Geta.Optimizely.GeolocationTools.Commerce.Models
{
    public interface ICommerceGeolocationResult
    {
        IMarket Market { get; set; }
        CultureInfo Language { get; set; }
        IGeolocationResult Location { get; set; }
    }
}
