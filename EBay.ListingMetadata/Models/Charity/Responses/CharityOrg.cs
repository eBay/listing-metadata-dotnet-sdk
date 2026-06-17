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

namespace EBay.ListingMetadata.Models.Charity.Responses;

/// <summary>
/// Full details of a charitable organization
/// </summary>
public sealed record CharityOrg
{
    /// <summary>
    /// ID of the charitable organization
    /// </summary>
    [JsonPropertyName("charityOrgId")]
    public string? CharityOrgId { get; init; }

    /// <summary>
    /// Description of the charitable organization
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Location details of the charitable organization
    /// </summary>
    [JsonPropertyName("location")]
    public Location? Location { get; init; }

    /// <summary>
    /// Logo of the charitable organization
    /// </summary>
    [JsonPropertyName("logoImage")]
    public CharityImage? LogoImage { get; init; }

    /// <summary>
    /// Mission statement of the charitable organization
    /// </summary>
    [JsonPropertyName("missionStatement")]
    public string? MissionStatement { get; init; }

    /// <summary>
    /// Name of the charitable organization
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Registration ID for the charitable organization (EIN for US marketplace)
    /// </summary>
    [JsonPropertyName("registrationId")]
    public string? RegistrationId { get; init; }

    /// <summary>
    /// Link to the website for the charitable organization
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; init; }
}

/// <summary>
/// Search response containing charitable organizations
/// </summary>
public sealed record CharitySearchResponse
{
    /// <summary>
    /// List of charitable organizations that match the search criteria
    /// </summary>
    [JsonPropertyName("charityOrgs")]
    public List<CharityOrg>? CharityOrgs { get; init; }

    /// <summary>
    /// Relative path to the current set of results
    /// </summary>
    [JsonPropertyName("href")]
    public string? Href { get; init; }

    /// <summary>
    /// Number of items returned in a single page
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Relative path to the next set of results
    /// </summary>
    [JsonPropertyName("next")]
    public string? Next { get; init; }

    /// <summary>
    /// Number of items that will be skipped in the result set
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    /// <summary>
    /// Relative path to the previous set of results
    /// </summary>
    [JsonPropertyName("prev")]
    public string? Prev { get; init; }

    /// <summary>
    /// Total number of matches for the search criteria
    /// </summary>
    [JsonPropertyName("total")]
    public int? Total { get; init; }
}

/// <summary>
/// Physical location of a charitable organization
/// </summary>
public sealed record Location
{
    /// <summary>
    /// Address of the charitable organization
    /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; init; }

    /// <summary>
    /// Geo-coordinates of the charitable organization
    /// </summary>
    [JsonPropertyName("geoCoordinates")]
    public GeoCoordinates? GeoCoordinates { get; init; }
}

/// <summary>
/// Physical address
/// </summary>
public sealed record Address
{
    /// <summary>
    /// City of the charitable organization
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>
    /// State or province of the charitable organization
    /// </summary>
    [JsonPropertyName("stateOrProvince")]
    public string? StateOrProvince { get; init; }

    /// <summary>
    /// Postal code of the charitable organization
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>
    /// Two-letter ISO 3166 country code
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }
}

/// <summary>
/// Geographic coordinates
/// </summary>
public sealed record GeoCoordinates
{
    /// <summary>
    /// Latitude component of the geographic coordinate
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// Longitude component of the geographic coordinate
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Charity logo image
/// </summary>
public sealed record CharityImage
{
    /// <summary>
    /// Height of the logo image
    /// </summary>
    [JsonPropertyName("height")]
    public string? Height { get; init; }

    /// <summary>
    /// URL to the logo image location
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; init; }

    /// <summary>
    /// Width of the logo image
    /// </summary>
    [JsonPropertyName("width")]
    public string? Width { get; init; }
}
