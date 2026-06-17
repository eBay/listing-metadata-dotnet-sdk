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


using System.Net;
using EBay.ListingMetadata.Generated.Catalog;
using EBay.ListingMetadata.Generated.Taxonomy;
using EBay.ListingMetadata.Generated.Metadata;
using EBay.ListingMetadata.Generated.Charity;
using EBay.ListingMetadata.Clients.Rest;
using EBay.ListingMetadata.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace EBay.ListingMetadata;

/// <summary>
/// Main SDK client providing unified access to all eBay Listing Metadata APIs
/// </summary>
public sealed class ListingMetadataClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;

    /// <summary>
    /// Catalog API client for product catalog operations
    /// </summary>
    public CatalogClient Catalog { get; }

    /// <summary>
    /// Taxonomy API client for category tree operations
    /// </summary>
    public TaxonomyClient Taxonomy { get; }

    /// <summary>
    /// Metadata API client for marketplace metadata operations
    /// </summary>
    public MetadataClient Metadata { get; }

    /// <summary>
    /// Charity API client for charitable organization operations
    /// </summary>
    public CharityClient Charity { get; }

    /// <summary>
    /// Creates a new SDK client.
    /// </summary>
    /// <param name="config">SDK configuration</param>
    /// <param name="httpClient">Optional HttpClient (if not provided, a standalone client with diagnostic logging will be created)</param>
    public ListingMetadataClient(
        ListingMetadataConfiguration config,
        HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? BuildStandaloneHttpClient(config);
        _disposeHttpClient = httpClient is null;

        var authProvider = BuildAuthProvider(
            ct => config.GetAccessTokenAsync(ct),
            config.GetAllowedHosts());

        var catalogAdapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
        catalogAdapter.BaseUrl = config.CatalogBaseUrl;

        var taxonomyAdapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
        taxonomyAdapter.BaseUrl = config.TaxonomyBaseUrl;

        var metadataAdapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
        metadataAdapter.BaseUrl = config.MetadataBaseUrl;

        var charityAdapter = new HttpClientRequestAdapter(authProvider, httpClient: _httpClient);
        charityAdapter.BaseUrl = config.CharityBaseUrl;

        Catalog = new CatalogClient(new CatalogApiClient(catalogAdapter));
        Taxonomy = new TaxonomyClient(new TaxonomyApiClient(taxonomyAdapter));
        Metadata = new MetadataClient(new MetadataApiClient(metadataAdapter));
        Charity = new CharityClient(new CharityApiClient(charityAdapter));
    }

    private static HttpClient BuildStandaloneHttpClient(ListingMetadataConfiguration config)
    {
        var handler = new DiagnosticLoggingHandler(config)
        {
            InnerHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) }
        };
        return new HttpClient(handler) { Timeout = config.HttpTimeout };
    }

    private static IAuthenticationProvider BuildAuthProvider(
        Func<CancellationToken, Task<string>> tokenProvider,
        IEnumerable<string> allowedHosts)
    {
        var accessTokenProvider = new DelegateAccessTokenProvider(tokenProvider, allowedHosts);
        return new BaseBearerTokenAuthenticationProvider(accessTokenProvider);
    }

    /// <summary>
    /// Disposes the SDK client and releases resources
    /// </summary>
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient?.Dispose();
        }
    }
}
