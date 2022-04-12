// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Net;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools
{
    /// <summary>
    /// Helper class to retrieve correct IP address from request
    /// </summary>
    internal static class IpAddressHelper
    {
        public static IPAddress GetRequestIpAddress(HttpRequest request)
        {
            var test = GetDevIpAddress(request);
            return !string.IsNullOrEmpty(test) ? ParseIpAddress(test) : request.HttpContext.Connection.RemoteIpAddress;
        }

        private static IPAddress ParseIpAddress(string address)
        {
            return IPAddress.TryParse(address, out var ipAddress)
                ? ipAddress
                : IPAddress.None;
        }

        private static string GetDevIpAddress(HttpRequest request)
        {
            return request?.Cookies[Constants.IpAddressOverride];
        }
    }
}
