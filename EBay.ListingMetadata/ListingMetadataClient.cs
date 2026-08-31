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
    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;

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

        var tokenContext = new AsyncLocalTokenContext();
        InitializeRestClients(authProvider, _httpClient, config, tokenContext);
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
