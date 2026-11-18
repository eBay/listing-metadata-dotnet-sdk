using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EBay.ListingMetadata;
using EBay.ListingMetadata.Configuration;
using EBay.ListingMetadata.Exceptions;

namespace EBay.ListingMetadata.CSharp73
{
    /// <summary>Typical consumer code written in C# 7.3; it only has to compile.</summary>
    public static class ConsumerUsage
    {
        public static async Task<string> RunAsync()
        {
            var config = new ListingMetadataConfiguration
            {
                AccessToken = "token",
                HttpTimeout = TimeSpan.FromSeconds(60),
                DiagnosticLogging = true,
                TaxonomyBaseUrl = "https://ebay-proxy.example.com/commerce/taxonomy/v1",
                AllowedHosts = { "ebay-proxy.example.com" },
                // Qualified: an unused EBay.ListingMetadata.RequestOptions (pre-existing) shadows the real type.
                DefaultRequestOptions = new Configuration.RequestOptions
                {
                    Headers = new Dictionary<string, string> { { "X-EBAY-C-MARKETPLACE-ID", "EBAY_US" } }
                }
            };

            using (var client = new ListingMetadataClient(config))
            {
                try
                {
                    var result = await client.Taxonomy.GetCategoryTreeAsync("0");
                    return result != null ? "ok" : "empty";
                }
                catch (UnauthorizedException)
                {
                    return "unauthorized";
                }
            }
        }
    }
}
