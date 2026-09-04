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


using EBay.ListingMetadata.Clients.Rest;
using EBay.ListingMetadata.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBay.ListingMetadata.Extensions;

/// <summary>
/// Dependency injection extensions for ListingMetadataSdk
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds ListingMetadataSdk clients to the service collection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">SDK configuration instance</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddListingMetadataSdk(
        this IServiceCollection services,
        ListingMetadataConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<ITokenContext, AsyncLocalTokenContext>();

        services.AddHttpClient(ListingMetadataClient.CatalogClientName,
                (sp, c) => c.BaseAddress = new Uri(sp.GetRequiredService<ListingMetadataConfiguration>().CatalogBaseUrl))
            .ConfigureHttpClient((sp, c) => c.Timeout = sp.GetRequiredService<ListingMetadataConfiguration>().HttpTimeout)
            .AddHttpMessageHandler(sp => new DiagnosticLoggingHandler(sp.GetRequiredService<ListingMetadataConfiguration>()));
        services.AddHttpClient(ListingMetadataClient.CharityClientName,
                (sp, c) => c.BaseAddress = new Uri(sp.GetRequiredService<ListingMetadataConfiguration>().CharityBaseUrl))
            .ConfigureHttpClient((sp, c) => c.Timeout = sp.GetRequiredService<ListingMetadataConfiguration>().HttpTimeout)
            .AddHttpMessageHandler(sp => new DiagnosticLoggingHandler(sp.GetRequiredService<ListingMetadataConfiguration>()));
        services.AddHttpClient(ListingMetadataClient.MetadataClientName,
                (sp, c) => c.BaseAddress = new Uri(sp.GetRequiredService<ListingMetadataConfiguration>().MetadataBaseUrl))
            .ConfigureHttpClient((sp, c) => c.Timeout = sp.GetRequiredService<ListingMetadataConfiguration>().HttpTimeout)
            .AddHttpMessageHandler(sp => new DiagnosticLoggingHandler(sp.GetRequiredService<ListingMetadataConfiguration>()));
        services.AddHttpClient(ListingMetadataClient.TaxonomyClientName,
                (sp, c) => c.BaseAddress = new Uri(sp.GetRequiredService<ListingMetadataConfiguration>().TaxonomyBaseUrl))
            .ConfigureHttpClient((sp, c) => c.Timeout = sp.GetRequiredService<ListingMetadataConfiguration>().HttpTimeout)
            .AddHttpMessageHandler(sp => new DiagnosticLoggingHandler(sp.GetRequiredService<ListingMetadataConfiguration>()));

        services.AddScoped<ListingMetadataClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return new ListingMetadataClient(
                sp.GetRequiredService<ListingMetadataConfiguration>(),
                factory.CreateClient(ListingMetadataClient.CatalogClientName),
                factory.CreateClient(ListingMetadataClient.CharityClientName),
                factory.CreateClient(ListingMetadataClient.MetadataClientName),
                factory.CreateClient(ListingMetadataClient.TaxonomyClientName),
                sp.GetRequiredService<ITokenContext>());
        });

        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Catalog);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Taxonomy);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Metadata);
        services.AddScoped(sp => sp.GetRequiredService<ListingMetadataClient>().Charity);

        return services;
    }
}
