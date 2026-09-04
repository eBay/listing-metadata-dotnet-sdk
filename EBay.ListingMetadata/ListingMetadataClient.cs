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


using EBay.ListingMetadata.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;

namespace EBay.ListingMetadata;

/// <summary>
/// Main SDK client providing unified access to all eBay Listing Metadata APIs
/// </summary>
public partial class ListingMetadataClient : IDisposable
{
    internal const string CatalogClientName = "ListingMetadataClient.Catalog";
    internal const string CharityClientName = "ListingMetadataClient.Charity";
    internal const string MetadataClientName = "ListingMetadataClient.Metadata";
    internal const string TaxonomyClientName = "ListingMetadataClient.Taxonomy";

    private readonly HttpClient[] _ownedClients;

    /// <summary>
    /// DI-friendly constructor: each HttpClient is a named client from IHttpClientFactory
    /// with DiagnosticLoggingHandler and BaseAddress already configured.
    /// </summary>
    internal ListingMetadataClient(
        ListingMetadataConfiguration config,
        HttpClient catalogClient,
        HttpClient charityClient,
        HttpClient metadataClient,
        HttpClient taxonomyClient,
        ITokenContext tokenContext)
    {
        _ownedClients = [];

        var auth = new BaseBearerTokenAuthenticationProvider(new KiotaAuthProvider(config, tokenContext));

        InitializeRestClients(auth, catalogClient, charityClient, metadataClient, taxonomyClient, config, tokenContext);
    }

    /// <summary>
    /// Standalone constructor for use outside DI (tests, scripts).
    /// Creates its own HttpClients with DiagnosticLoggingHandler.
    /// </summary>
    public ListingMetadataClient(ListingMetadataConfiguration config)
    {
        var tokenContext = new AsyncLocalTokenContext();
        var auth = new BaseBearerTokenAuthenticationProvider(new KiotaAuthProvider(config, tokenContext));

        var catalogClient = BuildStandaloneHttpClient(config, config.CatalogBaseUrl);
        var charityClient = BuildStandaloneHttpClient(config, config.CharityBaseUrl);
        var metadataClient = BuildStandaloneHttpClient(config, config.MetadataBaseUrl);
        var taxonomyClient = BuildStandaloneHttpClient(config, config.TaxonomyBaseUrl);
        _ownedClients = [catalogClient, charityClient, metadataClient, taxonomyClient];

        InitializeRestClients(auth, catalogClient, charityClient, metadataClient, taxonomyClient, config, tokenContext);
    }

    private static HttpClient BuildStandaloneHttpClient(ListingMetadataConfiguration config, string baseUrl)
    {
        var handler = new DiagnosticLoggingHandler(config)
        {
            InnerHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) }
        };
        return new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = config.HttpTimeout };
    }

    /// <summary>
    /// Disposes the SDK client and releases resources
    /// </summary>
    public void Dispose()
    {
        foreach (var client in _ownedClients)
            client.Dispose();
    }
}
