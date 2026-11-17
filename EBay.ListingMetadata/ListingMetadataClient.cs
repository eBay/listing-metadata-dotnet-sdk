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
using EBay.ListingMetadata.Generated.Catalog;
using EBay.ListingMetadata.Generated.Charity;
using EBay.ListingMetadata.Generated.Metadata;
using EBay.ListingMetadata.Generated.Taxonomy;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace EBay.ListingMetadata;

/// <summary>
/// Entry point for the Listing Metadata APIs. The client owns its HTTP connections:
/// create one per application (or register it with <c>AddListingMetadataSdk</c>, which adds it
/// as a singleton) and dispose it on shutdown. Reach the individual APIs through its properties.
/// </summary>
public class ListingMetadataClient : IDisposable
{
    private readonly HttpClient[] _ownedClients;

    public CatalogClient Catalog { get; private set; } = null!;
    public CharityClient Charity { get; private set; } = null!;
    public MetadataClient Metadata { get; private set; } = null!;
    public TaxonomyClient Taxonomy { get; private set; } = null!;

    /// <summary>Creates a client with its own HTTP connections.</summary>
    public ListingMetadataClient(ListingMetadataConfiguration config)
        : this(config, CreateDefaultPrimaryHandler, disposeHandlers: true)
    {
    }

    /// <summary>
    /// Creates a client that sends every request through <paramref name="primaryHandler"/>, for example to
    /// configure a proxy or client certificates, or to substitute a test double. The handler is shared by
    /// all APIs and is not disposed by this client.
    /// </summary>
    public ListingMetadataClient(ListingMetadataConfiguration config, HttpMessageHandler primaryHandler)
        : this(config, RequireHandler(primaryHandler), disposeHandlers: false)
    {
    }

    private ListingMetadataClient(
        ListingMetadataConfiguration config,
        Func<HttpMessageHandler> primaryHandler,
        bool disposeHandlers)
    {
        config.ValidateTrustedHosts();
        var tokenContext = new AsyncLocalTokenContext();
        var auth = new BaseBearerTokenAuthenticationProvider(new KiotaAuthProvider(config, tokenContext));
        var owned = new List<HttpClient>();
        HttpClient Build(DelegatingHandler logging, string baseUrl)
        {
            logging.InnerHandler = primaryHandler();
            var client = new HttpClient(logging, disposeHandlers)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = config.HttpTimeout
            };
            owned.Add(client);
            return client;
        }
        InitializeRestClients(auth, config, tokenContext, endpoint => Build(new DiagnosticLoggingHandler(config), endpoint.BaseUrl));
        _ownedClients = [.. owned];
    }

    private static HttpMessageHandler CreateDefaultPrimaryHandler() =>
#if NETFRAMEWORK
        // .NET Framework defaults to 2 connections per host outside ASP.NET.
        new HttpClientHandler { MaxConnectionsPerServer = 64 };
#else
        new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) };
#endif

    private static Func<HttpMessageHandler> RequireHandler(HttpMessageHandler primaryHandler)
    {
#if NETFRAMEWORK
        if (primaryHandler is null)
        {
            throw new ArgumentNullException(nameof(primaryHandler));
        }
#else
        ArgumentNullException.ThrowIfNull(primaryHandler);
#endif
        return () => primaryHandler;
    }

    internal static IReadOnlyList<RestEndpoint> RestEndpoints(ListingMetadataConfiguration config) =>
    [
        new RestEndpoint("catalog", config.RestBaseUrl("catalog")),
        new RestEndpoint("charity", config.RestBaseUrl("charity")),
        new RestEndpoint("metadata", config.RestBaseUrl("metadata")),
        new RestEndpoint("taxonomy", config.RestBaseUrl("taxonomy"))
    ];

    private void InitializeRestClients(
        IAuthenticationProvider auth,
        ListingMetadataConfiguration config,
        ITokenContext tokenContext,
        RestHttpClientProvider httpClientFor)
    {
        var endpoints = RestEndpoints(config);
        var catalogAdapter = new HttpClientRequestAdapter(auth, httpClient: httpClientFor(endpoints.First(endpoint => endpoint.ApiId == "catalog")));
        Catalog = new CatalogClient(new CatalogApiClient(catalogAdapter), config, tokenContext, catalogAdapter);
        var charityAdapter = new HttpClientRequestAdapter(auth, httpClient: httpClientFor(endpoints.First(endpoint => endpoint.ApiId == "charity")));
        Charity = new CharityClient(new CharityApiClient(charityAdapter), config, tokenContext, charityAdapter);
        var metadataAdapter = new HttpClientRequestAdapter(auth, httpClient: httpClientFor(endpoints.First(endpoint => endpoint.ApiId == "metadata")));
        Metadata = new MetadataClient(new MetadataApiClient(metadataAdapter), config, tokenContext, metadataAdapter);
        var taxonomyAdapter = new HttpClientRequestAdapter(auth, httpClient: httpClientFor(endpoints.First(endpoint => endpoint.ApiId == "taxonomy")));
        Taxonomy = new TaxonomyClient(new TaxonomyApiClient(taxonomyAdapter), config, tokenContext, taxonomyAdapter);
    }

    public void Dispose()
    {
        foreach (var client in _ownedClients)
        {
            client.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}

internal sealed record RestEndpoint(string ApiId, string BaseUrl);

internal delegate HttpClient RestHttpClientProvider(RestEndpoint endpoint);
