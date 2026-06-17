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
using EBay.ListingMetadata.Models.Common;

namespace EBay.ListingMetadata.Models.Metadata.Responses;

/// <summary>
/// Return policy response
/// </summary>
public sealed record ReturnPolicyResponse
{
    /// <summary>
    /// List of category IDs with return policy requirements
    /// </summary>
    [JsonPropertyName("returnPolicies")]
    public List<ReturnPolicy>? ReturnPolicies { get; init; }

    /// <summary>
    /// Warnings generated from the request
    /// </summary>
    [JsonPropertyName("warnings")]
    public List<Error>? Warnings { get; init; }
}

/// <summary>
/// Return policy for a category
/// </summary>
public sealed record ReturnPolicy
{
    /// <summary>
    /// Category ID
    /// </summary>
    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; init; }

    /// <summary>
    /// Indicates if domestic returns are accepted
    /// </summary>
    [JsonPropertyName("domestic")]
    public PolicyDetail? Domestic { get; init; }

    /// <summary>
    /// Indicates if international returns are accepted
    /// </summary>
    [JsonPropertyName("international")]
    public PolicyDetail? International { get; init; }

    /// <summary>
    /// Indicates if policy is required for the category
    /// </summary>
    [JsonPropertyName("required")]
    public bool? Required { get; init; }
}

/// <summary>
/// Policy detail information
/// </summary>
public sealed record PolicyDetail
{
    /// <summary>
    /// Indicates if returns are accepted
    /// </summary>
    [JsonPropertyName("returnsAccepted")]
    public bool? ReturnsAccepted { get; init; }

    /// <summary>
    /// Additional policy information
    /// </summary>
    [JsonPropertyName("policyDescription")]
    public string? PolicyDescription { get; init; }
}

/// <summary>
/// Shipping policies response
/// </summary>
public sealed record ShippingPoliciesResponse
{
    /// <summary>
    /// Policy metadata for leaf categories
    /// </summary>
    [JsonPropertyName("shippingPolicies")]
    public List<ShippingPolicy>? ShippingPolicies { get; init; }

    /// <summary>
    /// Warnings generated from the request
    /// </summary>
    [JsonPropertyName("warnings")]
    public List<Error>? Warnings { get; init; }
}

/// <summary>
/// Shipping policy for a category
/// </summary>
public sealed record ShippingPolicy
{
    /// <summary>
    /// Category ID
    /// </summary>
    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; init; }

    /// <summary>
    /// Indicates if category includes eBay delivery
    /// </summary>
    [JsonPropertyName("includeseBayDelivery")]
    public bool? IncludesEBayDelivery { get; init; }

    /// <summary>
    /// Regional shipping details
    /// </summary>
    [JsonPropertyName("regionalShippingOptions")]
    public RegionalShippingOptions? RegionalShippingOptions { get; init; }
}

/// <summary>
/// Regional shipping options
/// </summary>
public sealed record RegionalShippingOptions
{
    /// <summary>
    /// Domestic shipping options
    /// </summary>
    [JsonPropertyName("domestic")]
    public bool? Domestic { get; init; }

    /// <summary>
    /// International shipping options
    /// </summary>
    [JsonPropertyName("international")]
    public bool? International { get; init; }
}

/// <summary>
/// Item condition policy response
/// </summary>
public sealed record ItemConditionPolicyResponse
{
    /// <summary>
    /// Category IDs with condition indication policies
    /// </summary>
    [JsonPropertyName("itemConditionPolicies")]
    public List<ItemConditionPolicy>? ItemConditionPolicies { get; init; }

    /// <summary>
    /// Warnings generated from the request
    /// </summary>
    [JsonPropertyName("warnings")]
    public List<Error>? Warnings { get; init; }
}

/// <summary>
/// Item condition policy for a category
/// </summary>
public sealed record ItemConditionPolicy
{
    /// <summary>
    /// Category ID
    /// </summary>
    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; init; }

    /// <summary>
    /// Supported condition values
    /// </summary>
    [JsonPropertyName("conditionValues")]
    public List<ConditionValue>? ConditionValues { get; init; }
}

/// <summary>
/// Condition value with ID and description
/// </summary>
public sealed record ConditionValue
{
    /// <summary>
    /// Condition ID
    /// </summary>
    [JsonPropertyName("conditionId")]
    public string? ConditionId { get; init; }

    /// <summary>
    /// Condition description
    /// </summary>
    [JsonPropertyName("conditionDescription")]
    public string? ConditionDescription { get; init; }
}

/// <summary>
/// Shipping carrier response
/// </summary>
public sealed record ShippingCarrierResponse
{
    /// <summary>
    /// List of available shipping carriers for the marketplace
    /// </summary>
    [JsonPropertyName("shippingCarriers")]
    public List<ShippingCarrier>? ShippingCarriers { get; init; }
}

/// <summary>
/// Shipping carrier details
/// </summary>
public sealed record ShippingCarrier
{
    /// <summary>
    /// Shipping carrier code
    /// </summary>
    [JsonPropertyName("shippingCarrierCode")]
    public string? ShippingCarrierCode { get; init; }

    /// <summary>
    /// Shipping carrier name
    /// </summary>
    [JsonPropertyName("shippingCarrierName")]
    public string? ShippingCarrierName { get; init; }
}

/// <summary>
/// Currencies response
/// </summary>
public sealed record GetCurrenciesResponse
{
    /// <summary>
    /// Default currency used by the marketplace
    /// </summary>
    [JsonPropertyName("defaultCurrency")]
    public Currency? DefaultCurrency { get; init; }

    /// <summary>
    /// eBay marketplace ID
    /// </summary>
    [JsonPropertyName("marketplaceId")]
    public string? MarketplaceId { get; init; }
}

/// <summary>
/// Currency information
/// </summary>
public sealed record Currency
{
    /// <summary>
    /// Three-letter ISO 4217 currency code
    /// </summary>
    [JsonPropertyName("currency")]
    public string? CurrencyCode { get; init; }

    /// <summary>
    /// Currency description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
