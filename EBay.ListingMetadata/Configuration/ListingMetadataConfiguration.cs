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


namespace EBay.ListingMetadata.Configuration;

/// <summary>
/// Configuration for the ListingMetadata SDK
/// </summary>
public sealed class ListingMetadataConfiguration
{
    private string _catalogBaseUrl = "https://api.ebay.com/commerce/catalog/v1_beta";
    private string _taxonomyBaseUrl = "https://api.ebay.com/commerce/taxonomy/v1";
    private string _metadataBaseUrl = "https://api.ebay.com/sell/metadata/v1";
    private string _charityBaseUrl = "https://api.ebay.com/commerce/charity/v1";

    /// <summary>
    /// Base URL for Catalog API (default: https://api.ebay.com/commerce/catalog/v1_beta)
    /// </summary>
    public string CatalogBaseUrl
    {
        get => _catalogBaseUrl;
        set => _catalogBaseUrl = RequireHttps(value);
    }

    /// <summary>
    /// Base URL for Taxonomy API (default: https://api.ebay.com/commerce/taxonomy/v1)
    /// </summary>
    public string TaxonomyBaseUrl
    {
        get => _taxonomyBaseUrl;
        set => _taxonomyBaseUrl = RequireHttps(value);
    }

    /// <summary>
    /// Base URL for Metadata API (default: https://api.ebay.com/sell/metadata/v1)
    /// </summary>
    public string MetadataBaseUrl
    {
        get => _metadataBaseUrl;
        set => _metadataBaseUrl = RequireHttps(value);
    }

    /// <summary>
    /// Base URL for Charity API (default: https://api.ebay.com/commerce/charity/v1)
    /// </summary>
    public string CharityBaseUrl
    {
        get => _charityBaseUrl;
        set => _charityBaseUrl = RequireHttps(value);
    }

    /// <summary>
    /// Fallback bearer token used when no per-call accessToken is supplied.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>Timeout applied to all HTTP requests. Defaults to 30 seconds.</summary>
    public TimeSpan HttpTimeout { get; init; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// When true, logs the HTTP method, URL, and status code of non-success responses to stderr.
    /// Response bodies are intentionally excluded to avoid leaking PII.
    /// <para>
    /// <b>WARNING — PII risk.</b> URLs may contain item IDs and query parameters that are
    /// sensitive in context. Never enable this in production. Ensure logs are access-controlled
    /// and not persisted to any external system when enabled in non-production environments.
    /// </para>
    /// </summary>
    public bool DiagnosticLogging { get; set; }

    /// <summary>Default headers applied to every REST request. Per-call RequestOptions override these.</summary>
    public RequestOptions DefaultRequestOptions { get; set; } = new();

    internal IEnumerable<string> AllowedHosts =>
    [
        new Uri(_catalogBaseUrl).Host,
        new Uri(_charityBaseUrl).Host,
        new Uri(_metadataBaseUrl).Host,
        new Uri(_taxonomyBaseUrl).Host,
        "apiz.ebay.com",
    ];

    /// <summary>
    /// Gets allowed hosts for API token validation
    /// </summary>
    public IEnumerable<string> GetAllowedHosts() => AllowedHosts;

    private static string RequireHttps(string value)
    {
        if (!value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"URL must use HTTPS: '{value}'", nameof(value));
        return value;
    }
}
