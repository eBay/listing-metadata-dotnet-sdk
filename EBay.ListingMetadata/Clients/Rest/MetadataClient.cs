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


using EBay.ListingMetadata.Generated.Metadata;

namespace EBay.ListingMetadata.Clients.Rest;

/// <summary>
/// Client for Metadata API operations
/// </summary>
public sealed class MetadataClient
{
    private readonly MetadataApiClient _kiota;

    internal MetadataClient(MetadataApiClient kiota)
    {
        _kiota = kiota;
    }

    /// <summary>
    /// Retrieves return policies for a marketplace
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Return policies for the marketplace</returns>
    public async Task<EBay.ListingMetadata.Generated.Metadata.Models.ReturnPolicyResponse?> GetReturnPoliciesAsync(
        string marketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Marketplace[marketplaceId].Get_return_policies.GetAsync(cfg =>
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
    /// Retrieves shipping policies for a marketplace
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Shipping policies for the marketplace</returns>
    public async Task<EBay.ListingMetadata.Generated.Metadata.Models.ShippingPoliciesResponse?> GetShippingPoliciesAsync(
        string marketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Marketplace[marketplaceId].Get_shipping_policies.GetAsync(cfg =>
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
    /// Retrieves item condition policies for a marketplace and category
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="categoryId">Optional category ID to filter policies</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Item condition policies</returns>
    public async Task<EBay.ListingMetadata.Generated.Metadata.Models.ItemConditionPolicyResponse?> GetItemConditionPoliciesAsync(
        string marketplaceId,
        string? categoryId = null,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Marketplace[marketplaceId].Get_item_condition_policies.GetAsync(cfg =>
        {
            if (categoryId != null)
            {
                cfg.QueryParameters.Filter = $"categoryIds:{{{categoryId}}}";
            }

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
    /// Retrieves shipping carriers for a marketplace
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Shipping carriers available in the marketplace</returns>
    public async Task<EBay.ListingMetadata.Generated.Metadata.Models.ShippingCarrierResponse?> GetShippingCarriersAsync(
        string marketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Shipping.Marketplace[marketplaceId].Get_shipping_carriers.GetAsync(cfg =>
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
    /// Retrieves currencies supported by a marketplace
    /// </summary>
    /// <param name="marketplaceId">eBay marketplace ID (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Currencies supported in the marketplace</returns>
    public async Task<EBay.ListingMetadata.Generated.Metadata.Models.GetCurrenciesResponse?> GetCurrenciesAsync(
        string marketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Marketplace[marketplaceId].Get_currencies.GetAsync(cfg =>
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
}
