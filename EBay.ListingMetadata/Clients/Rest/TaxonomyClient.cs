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


using EBay.ListingMetadata.Generated.Taxonomy;
using Microsoft.Kiota.Abstractions;

namespace EBay.ListingMetadata.Clients.Rest;

/// <summary>
/// Client for Taxonomy API operations
/// </summary>
public sealed class TaxonomyClient
{
    private readonly TaxonomyApiClient _kiota;

    internal TaxonomyClient(TaxonomyApiClient kiota)
    {
        _kiota = kiota;
    }

    /// <summary>
    /// Retrieves the default category tree ID for a specified marketplace
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Default category tree information</returns>
    public async Task<EBay.ListingMetadata.Generated.Taxonomy.Models.BaseCategoryTree?> GetDefaultCategoryTreeIdAsync(
        string marketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Get_default_category_tree_id.GetAsync(cfg =>
        {
            cfg.QueryParameters.MarketplaceId = marketplaceId;

            if (options?.Headers != null)
            {
                foreach (var (key, value) in options.Headers)
                {
                    cfg.Headers.Add(key, value);
                }
            }

            if (!string.IsNullOrEmpty(options?.AccessToken))
            {
                cfg.Headers.Add("Authorization", $"Bearer {options.AccessToken}");
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Retrieves the complete category tree for a specified category tree ID
    /// </summary>
    /// <param name="categoryTreeId">Category tree ID</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Complete category tree</returns>
    public async Task<EBay.ListingMetadata.Generated.Taxonomy.Models.CategoryTree?> GetCategoryTreeAsync(
        string categoryTreeId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Category_tree[categoryTreeId].GetAsync(cfg =>
        {
            if (options?.Headers != null)
            {
                foreach (var (key, value) in options.Headers)
                {
                    cfg.Headers.Add(key, value);
                }
            }

            if (!string.IsNullOrEmpty(options?.AccessToken))
            {
                cfg.Headers.Add("Authorization", $"Bearer {options.AccessToken}");
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Retrieves category suggestions for a product query
    /// </summary>
    /// <param name="categoryTreeId">Category tree ID</param>
    /// <param name="query">Product query string</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Category suggestions</returns>
    public async Task<EBay.ListingMetadata.Generated.Taxonomy.Models.CategorySuggestionResponse?> GetCategorySuggestionsAsync(
        string categoryTreeId,
        string query,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Category_tree[categoryTreeId].Get_category_suggestions.GetAsync(cfg =>
        {
            cfg.QueryParameters.Q = query;

            if (options?.Headers != null)
            {
                foreach (var (key, value) in options.Headers)
                {
                    cfg.Headers.Add(key, value);
                }
            }

            if (!string.IsNullOrEmpty(options?.AccessToken))
            {
                cfg.Headers.Add("Authorization", $"Bearer {options.AccessToken}");
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Retrieves item aspects for a specific category
    /// </summary>
    /// <param name="categoryTreeId">Category tree ID</param>
    /// <param name="categoryId">Category ID</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Aspect metadata for the category</returns>
    public async Task<EBay.ListingMetadata.Generated.Taxonomy.Models.AspectMetadata?> GetItemAspectsForCategoryAsync(
        string categoryTreeId,
        string categoryId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Category_tree[categoryTreeId].Get_item_aspects_for_category.GetAsync(cfg =>
        {
            cfg.QueryParameters.CategoryId = categoryId;

            if (options?.Headers != null)
            {
                foreach (var (key, value) in options.Headers)
                {
                    cfg.Headers.Add(key, value);
                }
            }

            if (!string.IsNullOrEmpty(options?.AccessToken))
            {
                cfg.Headers.Add("Authorization", $"Bearer {options.AccessToken}");
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Retrieves a category subtree starting from a specified category
    /// </summary>
    /// <param name="categoryTreeId">Category tree ID</param>
    /// <param name="categoryId">Category ID for subtree root</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Category subtree</returns>
    public async Task<EBay.ListingMetadata.Generated.Taxonomy.Models.CategorySubtree?> GetCategorySubtreeAsync(
        string categoryTreeId,
        string categoryId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Category_tree[categoryTreeId].Get_category_subtree.GetAsync(cfg =>
        {
            cfg.QueryParameters.CategoryId = categoryId;

            if (options?.Headers != null)
            {
                foreach (var (key, value) in options.Headers)
                {
                    cfg.Headers.Add(key, value);
                }
            }

            if (!string.IsNullOrEmpty(options?.AccessToken))
            {
                cfg.Headers.Add("Authorization", $"Bearer {options.AccessToken}");
            }
        }, cancellationToken);
    }
}
