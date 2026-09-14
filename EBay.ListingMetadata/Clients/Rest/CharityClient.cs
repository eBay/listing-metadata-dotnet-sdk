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
using EBay.ListingMetadata.Generated.Charity.Models;
using EBay.ListingMetadata.Generated.Charity;
using Microsoft.Kiota.Abstractions;

namespace EBay.ListingMetadata.Clients.Rest;

public sealed class CharityClient
{
    private readonly CharityApiClient _kiota;
    private readonly ListingMetadataConfiguration _config;
    private readonly ITokenContext _tokenContext;
    private readonly IRequestAdapter _adapter;

    internal CharityClient(CharityApiClient kiota, ListingMetadataConfiguration config, ITokenContext tokenContext, IRequestAdapter adapter)
    {
        _kiota = kiota;
        _config = config;
        _tokenContext = tokenContext;
        _adapter = adapter;
    }

    /// <summary>
    /// This call is used to retrieve detailed information about supported charitable organizations. It allows users to retrieve the details for a specific charitable organization using its charity organization ID.
    /// </summary>
    public Task<CharityOrg?> GetCharityOrgAsync(
        string charityOrgId,
        string xEBAYCMARKETPLACEID,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Charity_org[charityOrgId].GetAsync(cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This call is used to search for supported charitable organizations. It allows users to search for a specific charitable organization, or for multiple charitable organizations, from a particular charitable domain and/or geographical region, or by using search criteria.
    /// The call returns paginated search results containing the charitable organizations that match the specified criteria.
    /// </summary>
    public Task<CharitySearchResponse?> GetCharityOrgsAsync(
        string xEBAYCMARKETPLACEID,
        string? limit = null,
        string? offset = null,
        string? q = null,
        string? registrationIds = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Charity_org.GetAsync(cfg =>
        {
            if (limit is not null)
            {
                cfg.QueryParameters.Limit = limit;
            }
            if (offset is not null)
            {
                cfg.QueryParameters.Offset = offset;
            }
            if (q is not null)
            {
                cfg.QueryParameters.Q = q;
            }
            if (registrationIds is not null)
            {
                cfg.QueryParameters.RegistrationIds = registrationIds;
            }
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));
}
