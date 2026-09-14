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
using EBay.ListingMetadata.Generated.Taxonomy.Models;
using EBay.ListingMetadata.Generated.Taxonomy;
using Microsoft.Kiota.Abstractions;

namespace EBay.ListingMetadata.Clients.Rest;

public sealed class TaxonomyClient
{
    private readonly TaxonomyApiClient _kiota;
    private readonly ListingMetadataConfiguration _config;
    private readonly ITokenContext _tokenContext;
    private readonly IRequestAdapter _adapter;

    internal TaxonomyClient(TaxonomyApiClient kiota, ListingMetadataConfiguration config, ITokenContext tokenContext, IRequestAdapter adapter)
    {
        _kiota = kiota;
        _config = config;
        _tokenContext = tokenContext;
        _adapter = adapter;
    }

    /// <summary>
    /// Get item aspects for all leaf categories in a marketplace
    /// </summary>
    public Task<GetCategoriesAspectResponse?> FetchItemAspectsAsync(
        string categoryTreeId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Fetch_item_aspects.GetAsync(cfg =>
        {
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get the default category tree ID for a marketplace
    /// </summary>
    public Task<BaseCategoryTree?> GetDefaultCategoryTreeIdAsync(
        string marketplaceId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Get_default_category_tree_id.GetAsync(cfg =>
        {
            cfg.QueryParameters.MarketplaceId = marketplaceId;
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get a category tree
    /// </summary>
    public Task<CategoryTree?> GetCategoryTreeAsync(
        string categoryTreeId,
        string? acceptEncoding = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].GetAsync(cfg =>
        {
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get a category subtree
    /// </summary>
    public Task<CategorySubtree?> GetCategorySubtreeAsync(
        string categoryId,
        string categoryTreeId,
        string? acceptEncoding = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_category_subtree.GetAsync(cfg =>
        {
            cfg.QueryParameters.CategoryId = categoryId;
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get suggested categories for a keyword
    /// </summary>
    public Task<CategorySuggestionResponse?> GetCategorySuggestionsAsync(
        string categoryTreeId,
        string q,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_category_suggestions.GetAsync(cfg =>
        {
            cfg.QueryParameters.Q = q;
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get aspects for a leaf category
    /// </summary>
    public Task<AspectMetadata?> GetItemAspectsForCategoryAsync(
        string categoryId,
        string categoryTreeId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_item_aspects_for_category.GetAsync(cfg =>
        {
            cfg.QueryParameters.CategoryId = categoryId;
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get compatibility properties for a compatibility-enabled category
    /// </summary>
    public Task<GetCompatibilityMetadataResponse?> GetCompatibilityPropertiesAsync(
        string categoryTreeId,
        string categoryId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_compatibility_properties.GetAsync(cfg =>
        {
            cfg.QueryParameters.CategoryId = categoryId;
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get compatibility property values for a compatibility-enabled category
    /// </summary>
    public Task<GetCompatibilityPropertyValuesResponse?> GetCompatibilityPropertyValuesAsync(
        string categoryTreeId,
        string compatibilityProperty,
        string categoryId,
        string? filter = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_compatibility_property_values.GetAsync(cfg =>
        {
            cfg.QueryParameters.CompatibilityProperty = compatibilityProperty;
            cfg.QueryParameters.CategoryId = categoryId;
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// Get expired categories for a marketplace
    /// </summary>
    public Task<ExpiredCategories?> GetExpiredCategoriesAsync(
        string categoryTreeId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Category_tree[categoryTreeId].Get_expired_categories.GetAsync(cfg =>
        {
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));
}
