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

namespace EBay.ListingMetadata;

public partial class ListingMetadataClient
{
    /// <summary>
    /// Catalog API client for product catalog operations
    /// </summary>
    public CatalogClient Catalog { get; private set; } = null!;

    /// <summary>
    /// Taxonomy API client for category tree operations
    /// </summary>
    public TaxonomyClient Taxonomy { get; private set; } = null!;

    /// <summary>
    /// Metadata API client for marketplace metadata operations
    /// </summary>
    public MetadataClient Metadata { get; private set; } = null!;

    /// <summary>
    /// Charity API client for charitable organization operations
    /// </summary>
    public CharityClient Charity { get; private set; } = null!;

    private void InitializeRestClients(
        IAuthenticationProvider authProvider,
        HttpClient httpClient,
        ListingMetadataConfiguration config,
        ITokenContext tokenContext)
    {
        _ = tokenContext;

        var catalogAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        catalogAdapter.BaseUrl = config.CatalogBaseUrl;

        var taxonomyAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        taxonomyAdapter.BaseUrl = config.TaxonomyBaseUrl;

        var metadataAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        metadataAdapter.BaseUrl = config.MetadataBaseUrl;

        var charityAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        charityAdapter.BaseUrl = config.CharityBaseUrl;

        Catalog = new CatalogClient(new CatalogApiClient(catalogAdapter));
        Taxonomy = new TaxonomyClient(new TaxonomyApiClient(taxonomyAdapter));
        Metadata = new MetadataClient(new MetadataApiClient(metadataAdapter));
        Charity = new CharityClient(new CharityApiClient(charityAdapter));
    }
}
