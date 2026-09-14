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

public class ListingMetadataClient : IDisposable
{
    internal const string CatalogClientName = "ListingMetadataClient.Catalog";
    internal const string CharityClientName = "ListingMetadataClient.Charity";
    internal const string MetadataClientName = "ListingMetadataClient.Metadata";
    internal const string TaxonomyClientName = "ListingMetadataClient.Taxonomy";

    private readonly HttpClient[] _ownedClients;

    public CatalogClient Catalog { get; private set; } = null!;
    public CharityClient Charity { get; private set; } = null!;
    public MetadataClient Metadata { get; private set; } = null!;
    public TaxonomyClient Taxonomy { get; private set; } = null!;

    /// <summary>
    /// DI-friendly constructor. Named HttpClients come from <see cref="IHttpClientFactory"/>.
    /// </summary>
    internal ListingMetadataClient(
        ListingMetadataConfiguration config,
        IHttpClientFactory factory,
        ITokenContext tokenContext)
    {
        _ownedClients = [];
        var auth = new BaseBearerTokenAuthenticationProvider(new KiotaAuthProvider(config, tokenContext));
        InitializeRestClients(auth, config, tokenContext, endpoint => factory.CreateClient(endpoint.HttpClientName));
    }

    /// <summary>
    /// Standalone constructor for use outside DI (tests, scripts).
    /// Creates its own HttpClients with DiagnosticLoggingHandler.
    /// </summary>
    public ListingMetadataClient(ListingMetadataConfiguration config)
    {
        var tokenContext = new AsyncLocalTokenContext();
        var auth = new BaseBearerTokenAuthenticationProvider(new KiotaAuthProvider(config, tokenContext));
        var owned = new List<HttpClient>();
        InitializeRestClients(auth, config, tokenContext, endpoint =>
        {
            var client = BuildStandaloneHttpClient(config, endpoint.BaseUrl);
            owned.Add(client);
            return client;
        });
        _ownedClients = [.. owned];
    }

    private static HttpClient BuildStandaloneHttpClient(ListingMetadataConfiguration config, string baseUrl)
    {
        var handler = new DiagnosticLoggingHandler(config)
        {
            InnerHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) }
        };
        return new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = config.HttpTimeout };
    }

    internal static IReadOnlyList<(string ApiId, string HttpClientName)> RestEndpointNames { get; } =
    [
        ("catalog", CatalogClientName),
        ("charity", CharityClientName),
        ("metadata", MetadataClientName),
        ("taxonomy", TaxonomyClientName)
    ];

    internal static IReadOnlyList<RestEndpoint> RestEndpoints(ListingMetadataConfiguration config) =>
        [.. RestEndpointNames.Select(name => new RestEndpoint(name.ApiId, name.HttpClientName, config.RestBaseUrl(name.ApiId)))];

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

internal sealed record RestEndpoint(string ApiId, string HttpClientName, string BaseUrl);

internal delegate HttpClient RestHttpClientProvider(RestEndpoint endpoint);
