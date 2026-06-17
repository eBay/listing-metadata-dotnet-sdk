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


using EBay.ListingMetadata.Generated.Charity;

namespace EBay.ListingMetadata.Clients.Rest;

/// <summary>
/// Client for Charity API operations
/// </summary>
public sealed class CharityClient
{
    private readonly CharityApiClient _kiota;

    internal CharityClient(CharityApiClient kiota)
    {
        _kiota = kiota;
    }

    /// <summary>
    /// Retrieves details of a specific charity organization
    /// </summary>
    /// <param name="charityOrgId">Charity organization ID</param>
    /// <param name="xEbayCMarketplaceId">eBay marketplace ID header (e.g., EBAY_US)</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Charity organization details</returns>
    public async Task<EBay.ListingMetadata.Generated.Charity.Models.CharityOrg?> GetCharityOrgAsync(
        string charityOrgId,
        string xEbayCMarketplaceId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Charity_org[charityOrgId].GetAsync(cfg =>
        {
            cfg.Headers.Add("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId);

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
    /// Searches for charity organizations
    /// </summary>
    /// <param name="xEbayCMarketplaceId">eBay marketplace ID header (e.g., EBAY_US)</param>
    /// <param name="query">Optional search query</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of charity organizations</returns>
    public async Task<EBay.ListingMetadata.Generated.Charity.Models.CharitySearchResponse?> GetCharityOrgsAsync(
        string xEbayCMarketplaceId,
        string? query = null,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Charity_org.GetAsync(cfg =>
        {
            cfg.Headers.Add("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId);
            if (query != null)
            {
                cfg.QueryParameters.Q = query;
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
}
