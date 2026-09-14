// Copyright 2026 eBay Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using EBay.ListingMetadata;
using EBay.ListingMetadata.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBay.ListingMetadata.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddListingMetadataSdk(
        this IServiceCollection services,
        ListingMetadataConfiguration config)
    {
        services.AddSingleton(config);
        services.AddSingleton<ITokenContext, AsyncLocalTokenContext>();

        foreach (var endpoint in ListingMetadataClient.RestEndpointNames)
        {
            var id = endpoint.ApiId;
            services.AddHttpClient(endpoint.HttpClientName, (sp, c) =>
                    c.BaseAddress = new Uri(sp.GetRequiredService<ListingMetadataConfiguration>().RestBaseUrl(id)))
                .ConfigureHttpClient((sp, c) =>
                    c.Timeout = sp.GetRequiredService<ListingMetadataConfiguration>().HttpTimeout)
                .AddHttpMessageHandler(sp =>
                    new DiagnosticLoggingHandler(sp.GetRequiredService<ListingMetadataConfiguration>()));
        }

        services.AddScoped<ListingMetadataClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return new ListingMetadataClient(
                sp.GetRequiredService<ListingMetadataConfiguration>(),
                factory,
                sp.GetRequiredService<ITokenContext>());
        });

        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Catalog);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Charity);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Metadata);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Taxonomy);
        return services;
    }
}
