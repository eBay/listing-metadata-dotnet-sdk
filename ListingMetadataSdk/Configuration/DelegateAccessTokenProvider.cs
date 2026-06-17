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


using Microsoft.Kiota.Abstractions.Authentication;

namespace EBay.ListingMetadata.Configuration;

/// <summary>
/// Kiota access token provider that delegates to a user-provided function
/// </summary>
internal sealed class DelegateAccessTokenProvider : IAccessTokenProvider
{
    private readonly Func<CancellationToken, Task<string>> _tokenProvider;

    public AllowedHostsValidator AllowedHostsValidator { get; }

    public DelegateAccessTokenProvider(
        Func<CancellationToken, Task<string>> tokenProvider,
        IEnumerable<string> allowedHosts)
    {
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        AllowedHostsValidator = new AllowedHostsValidator(allowedHosts);
    }

    public Task<string> GetAuthorizationTokenAsync(
        Uri uri,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        return _tokenProvider(cancellationToken);
    }
}
