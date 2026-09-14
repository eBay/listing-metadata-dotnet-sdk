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
using EBay.ListingMetadata.Generated.Catalog.Models;
using EBay.ListingMetadata.Generated.Catalog;
using Microsoft.Kiota.Abstractions;

namespace EBay.ListingMetadata.Clients.Rest;

public sealed class CatalogClient
{
    private readonly CatalogApiClient _kiota;
    private readonly ListingMetadataConfiguration _config;
    private readonly ITokenContext _tokenContext;
    private readonly IRequestAdapter _adapter;

    internal CatalogClient(CatalogApiClient kiota, ListingMetadataConfiguration config, ITokenContext tokenContext, IRequestAdapter adapter)
    {
        _kiota = kiota;
        _config = config;
        _tokenContext = tokenContext;
        _adapter = adapter;
    }

    /// <summary>
    /// This method retrieves details of the catalog product identified by the eBay product identifier (ePID) specified in the request. These details include the product's title and description, aspects and their values, associated images, applicable category IDs, and any recognized identifiers that apply to the product.
    /// For a new listing, you can use the <b>search</b> method to identify candidate products on which to base the listing, then use the <b>getProduct</b> method to present the full details of those candidate products to the seller to make a a final selection.
    /// </summary>
    public Task<Product?> GetProductAsync(
        string epid,
        string? xEBAYCMARKETPLACEID = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Product[epid].GetAsync(cfg =>
        {
            if (xEBAYCMARKETPLACEID is not null)
            {
                cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method searches for and retrieves summaries of one or more products in the eBay catalog that match the search criteria provided by a seller. The seller can use the summaries to select the product in the eBay catalog that corresponds to the item that the seller wants to offer for sale. When a corresponding product is found and adopted by the seller, eBay will use the product information to populate the item listing. The criteria supported by <b>search</b> include keywords, product categories, and category aspects. To see the full details of a selected product, use the <b>getProduct</b> call.
    /// In addition to product summaries, this method can also be used to identify <i>refinements</i>, which help you to better pinpoint the product you're looking for. A refinement consists of one or more <i>aspect</i> values and a count of the number of times that each value has been used in previous eBay listings. An aspect is a property (e.g. color or size) of an eBay category, used by sellers to provide details about the items they're listing. The <b>refinement</b> container is returned when you include the <b>fieldGroups</b> query parameter in the request with a value of <c>ASPECT_REFINEMENTS</c> or <c>FULL</c>.
    /// <b>Example</b>
    /// A seller wants to find a product that is "gray" in color, but doesn't know what term the manufacturer uses for that color. It might be <c>Silver</c>, <c>Brushed Nickel</c>, <c>Pewter</c>, or even <c>Grey</c>. The returned <b>refinement</b> container identifies all aspects that have been used in past listings for products that match your search criteria, along with all of the values those aspects have taken, and the number of times each value was used. You can use this data to present the seller with a histogram of the values of each aspect. The seller can see which color values have been used in the past, and how frequently they have been used, and selects the most likely value or values for their product. You issue the <b>search</b> method again with those values in the <b>aspect_filter</b> parameter to narrow down the collection of products returned by the call.
    /// Although all query parameters are optional, this method must include at least the <b>q</b> parameter, or the <b>category_ids</b>, <b>gtin</b>, or <b>mpn</b> parameter with a valid value. If you provide more than one of these parameters, they will be combined with a logical AND to further refine the returned collection of matching products.
    /// <b>Note:</b> This method requires that certain special characters in the query parameters be percent-encoded:
    /// <c>(space)</c> = <c>%20</c> <c>,</c> = <c>%2C</c> <c>:</c> = <c>%3A</c> <c>[</c> = <c>%5B</c> <c>]</c> = <c>%5D</c> <c>{</c> = <c>%7B</c> <c>|</c> = <c>%7C</c> <c>}</c> = <c>%7D</c>
    /// This requirement applies to all query parameter values. However, for readability, method examples and samples in this documentation will not use the encoding.
    /// This method returns product summaries rather than the full details of the products. To retrieve the full details of a product, use the <b>getProduct</b> method with an ePID.
    /// </summary>
    public Task<ProductSearchResponse?> SearchAsync(
        string? xEBAYCMARKETPLACEID = null,
        string? aspectFilter = null,
        string? categoryIds = null,
        string? fieldgroups = null,
        string? gtin = null,
        string? limit = null,
        string? mpn = null,
        string? offset = null,
        string? q = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Product_summary.Search.GetAsync(cfg =>
        {
            if (aspectFilter is not null)
            {
                cfg.QueryParameters.AspectFilter = aspectFilter;
            }
            if (categoryIds is not null)
            {
                cfg.QueryParameters.CategoryIds = categoryIds;
            }
            if (fieldgroups is not null)
            {
                cfg.QueryParameters.Fieldgroups = fieldgroups;
            }
            if (gtin is not null)
            {
                cfg.QueryParameters.Gtin = gtin;
            }
            if (limit is not null)
            {
                cfg.QueryParameters.Limit = limit;
            }
            if (mpn is not null)
            {
                cfg.QueryParameters.Mpn = mpn;
            }
            if (offset is not null)
            {
                cfg.QueryParameters.Offset = offset;
            }
            if (q is not null)
            {
                cfg.QueryParameters.Q = q;
            }
            if (xEBAYCMARKETPLACEID is not null)
            {
                cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));
}
