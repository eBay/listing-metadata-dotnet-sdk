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


using System.Text.Json.Serialization;

namespace EBay.ListingMetadata.Models.Common;

/// <summary>
/// Standard error response
/// </summary>
public sealed record Error
{
    /// <summary>
    /// Type of error
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; init; }

    /// <summary>
    /// Primary system where the error occurred
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; init; }

    /// <summary>
    /// Unique error identifier
    /// </summary>
    [JsonPropertyName("errorId")]
    public int? ErrorId { get; init; }

    /// <summary>
    /// Request elements associated to the error
    /// </summary>
    [JsonPropertyName("inputRefIds")]
    public List<string>? InputRefIds { get; init; }

    /// <summary>
    /// Detailed explanation of the error
    /// </summary>
    [JsonPropertyName("longMessage")]
    public string? LongMessage { get; init; }

    /// <summary>
    /// Brief error message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Request parameters associated to the error
    /// </summary>
    [JsonPropertyName("parameters")]
    public List<ErrorParameter>? Parameters { get; init; }
}

/// <summary>
/// Error parameter information
/// </summary>
public sealed record ErrorParameter
{
    /// <summary>
    /// Name of the parameter
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Value of the parameter
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}
