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


namespace EBay.ListingMetadata;

/// <summary>
/// Optional configuration for individual API requests
/// </summary>
public sealed class RequestOptions
{
    /// <summary>
    /// Override access token for this specific request (useful for multi-tenant scenarios)
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Additional custom headers to include in the request
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}
