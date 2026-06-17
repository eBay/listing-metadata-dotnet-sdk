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

namespace EBay.ListingMetadata.Models.Catalog.Responses;

/// <summary>
/// Contains the full details of a specified product
/// </summary>
public sealed record Product
{
    /// <summary>
    /// Additional images associated with this product
    /// </summary>
    [JsonPropertyName("additionalImages")]
    public List<Image>? AdditionalImages { get; init; }

    /// <summary>
    /// Category aspects and their values associated with this product
    /// </summary>
    [JsonPropertyName("aspects")]
    public List<Aspect>? Aspects { get; init; }

    /// <summary>
    /// Manufacturer's brand name for this product
    /// </summary>
    [JsonPropertyName("brand")]
    public string? Brand { get; init; }

    /// <summary>
    /// Number of distinct motor vehicles compatible with the product
    /// </summary>
    [JsonPropertyName("compatibilityCount")]
    public int? CompatibilityCount { get; init; }

    /// <summary>
    /// Rich description of this product
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// European Article Numbers (EANs) that identify this product
    /// </summary>
    [JsonPropertyName("ean")]
    public List<string>? Ean { get; init; }

    /// <summary>
    /// eBay product ID
    /// </summary>
    [JsonPropertyName("epid")]
    public string? Epid { get; init; }

    /// <summary>
    /// GTINs that identify this product
    /// </summary>
    [JsonPropertyName("gtin")]
    public List<string>? Gtin { get; init; }

    /// <summary>
    /// Primary image of this product
    /// </summary>
    [JsonPropertyName("image")]
    public Image? Image { get; init; }

    /// <summary>
    /// International Standard Book Numbers (ISBNs) that identify this product
    /// </summary>
    [JsonPropertyName("isbn")]
    public List<string>? Isbn { get; init; }

    /// <summary>
    /// Manufacturer Part Numbers (MPNs) that identify this product
    /// </summary>
    [JsonPropertyName("mpn")]
    public List<string>? Mpn { get; init; }

    /// <summary>
    /// Other applicable category IDs for this product
    /// </summary>
    [JsonPropertyName("otherApplicableCategoryIds")]
    public List<string>? OtherApplicableCategoryIds { get; init; }

    /// <summary>
    /// Primary category ID recommended by eBay
    /// </summary>
    [JsonPropertyName("primaryCategoryId")]
    public string? PrimaryCategoryId { get; init; }

    /// <summary>
    /// URL for this product's eBay product page
    /// </summary>
    [JsonPropertyName("productWebUrl")]
    public string? ProductWebUrl { get; init; }

    /// <summary>
    /// Title of this product on eBay
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Universal Product Codes (UPCs) that identify this product
    /// </summary>
    [JsonPropertyName("upc")]
    public List<string>? Upc { get; init; }

    /// <summary>
    /// Current version number of this product record
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }
}

/// <summary>
/// Product image information
/// </summary>
public sealed record Image
{
    /// <summary>
    /// Height of the image in pixels
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <summary>
    /// eBay Picture Services (EPS) URL of the image
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; init; }

    /// <summary>
    /// Width of the image in pixels
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; init; }
}

/// <summary>
/// Category aspect name and values
/// </summary>
public sealed record Aspect
{
    /// <summary>
    /// Localized name of this category aspect
    /// </summary>
    [JsonPropertyName("localizedName")]
    public string? LocalizedName { get; init; }

    /// <summary>
    /// Localized values of this category aspect
    /// </summary>
    [JsonPropertyName("localizedValues")]
    public List<string>? LocalizedValues { get; init; }
}
