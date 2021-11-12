using Microsoft.AspNetCore.Builder;

namespace Geta.Optimizely.GeolocationTools
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGeolocationTools(this IApplicationBuilder builder)
        {
            builder.UseForwardedHeaders();

            return builder;
        }

        public static IApplicationBuilder UseGeolocationTools(this IApplicationBuilder builder, ForwardedHeadersOptions options)
        {
            builder.UseForwardedHeaders(options);

            return builder;
        }
    }
}
