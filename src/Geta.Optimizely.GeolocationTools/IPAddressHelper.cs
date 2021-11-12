// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information

using System.Net;
using Microsoft.AspNetCore.Http;

namespace Geta.Optimizely.GeolocationTools
{
    /// <summary>
    /// Helper class to retrieve correct IP address from request
    /// </summary>
    internal static class IPAddressHelper
    {
        public static IPAddress GetRequestIpAddress(HttpRequest request)
        {
            var test = GetDevIPAddress(request);
            return !string.IsNullOrEmpty(test) ? ParseIpAddress(test) : request.HttpContext.Connection.RemoteIpAddress;
        }

        private static IPAddress ParseIpAddress(string address)
        {
            if (!IPAddress.TryParse(address, out var ipAddress))
            {
                return IPAddress.None;
            }

            return ipAddress;
        }

        private static string GetDevIPAddress(HttpRequest request)
        {
            return request?.Cookies[Constants.IPAddressOverride];
        }
    }
}
