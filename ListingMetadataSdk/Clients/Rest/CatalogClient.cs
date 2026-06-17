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


using EBay.ListingMetadata.Generated.Catalog;

namespace EBay.ListingMetadata.Clients.Rest;

/// <summary>
/// Client for Catalog API operations
/// </summary>
public sealed class CatalogClient
{
    private readonly CatalogApiClient _kiota;

    internal CatalogClient(CatalogApiClient kiota)
    {
        _kiota = kiota;
    }

    /// <summary>
    /// Retrieves details of a catalog product by its eBay product identifier (ePID)
    /// </summary>
    /// <param name="epid">eBay product ID</param>
    /// <param name="options">Optional request configuration (access token override, custom headers)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Product details</returns>
    public async Task<EBay.ListingMetadata.Generated.Catalog.Models.Product?> GetProductAsync(
        string epid,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return await _kiota.Product[epid].GetAsync(cfg =>
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
