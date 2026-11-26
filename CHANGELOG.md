# ChangeLog

## 0.5.0 (11/26/2026)

### Added

- Local mock servers: base URL properties accept plain `http` when the host is a loopback host (`127.0.0.1`, `::1` or `localhost`) that you also add to `ListingMetadataConfiguration.AllowedHosts`. Before, every base URL had to use HTTPS. Every other host still requires HTTPS. `ListingMetadataConfiguration.LoopbackHosts` lists the loopback hosts.

### Changed

- Hosts are compared case-insensitively and without IPv6 brackets, so `[::1]` and `::1` match.

## 0.4.0 (11/17/2026)

### Breaking changes

#### Metadata API changes

- `SpecificationResponse.CompatibilityDetails` is renamed to `Compatibilities`, following eBay's API specification.

#### Simpler dependency injection

- `AddListingMetadataSdk` now registers only the configuration and a singleton `ListingMetadataClient`. `CatalogClient`, `CharityClient`, `MetadataClient` and `TaxonomyClient` are no longer registered individually: inject `ListingMetadataClient` and use its `Catalog`, `Charity`, `Metadata` and `Taxonomy` properties.
- The SDK no longer uses `IHttpClientFactory` or registers named `HttpClient`s, and no longer depends on `Microsoft.Extensions.Http`. To customise HTTP (proxy, client certificates, test doubles), use the new `ListingMetadataClient(ListingMetadataConfiguration, HttpMessageHandler)` constructor.

#### Trusted hosts

- The client constructors throw `InvalidOperationException` when a base URL uses a host that is not trusted (see Security). Add such hosts, for example a proxy, to `ListingMetadataConfiguration.AllowedHosts`.

### Security

- The access token is sent only to trusted hosts. Before, it was attached to requests for any HTTPS host configured as a base URL.

### Added

- `ListingMetadataConfiguration.AllowedHosts` for additional hosts trusted with the access token, and `ListingMetadataConfiguration.DefaultAllowedHosts`. The eBay production hosts and their sandbox counterparts (for example `api.sandbox.ebay.com`) are trusted by default, so the sandbox needs no extra configuration.
- Target framework `net472` (.NET Framework 4.7.2).

### Changed

- Dependency minimums are lowered to the versions Kiota itself requires, so projects can keep older compatible versions: `Microsoft.Kiota.Bundle` >= 1.22.0 and `Microsoft.Extensions.DependencyInjection.Abstractions` >= 6.0.0. Previously the SDK required the newest 10.0.x packages. On .NET Framework the SDK requires `System.Text.Json` >= 10.0.0, matching the oldest supported .NET LTS (.NET 10). None of these minimums has a known vulnerability, and each release is tested at both the minimum and the latest versions.
- On .NET Framework the SDK's HttpClients allow 64 connections per host instead of the `ServicePointManager` default of 2.
- `ListingMetadataConfiguration.HttpTimeout` and `RequestOptions.Headers` have `set` accessors instead of `init`, so projects compiled with C# 7.3 can assign them. Existing source compiles unchanged; recompile code that was built against 0.3.0.
- Metadata API errors are now thrown as `ErrorMessage`, whose `Message` is eBay's first error message and whose `Errors` lists all of them. Before, these responses were not mapped and only the HTTP status was available.

## 0.3.0 (09/25/2026)

### Added

- Target frameworks `net8.0` and `net9.0`, in addition to `net10.0`.

## 0.2.0 (09/14/2026)

### Breaking changes

#### Changed signatures

`CatalogClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetProductAsync` | `(string epid, RequestOptions? options, CancellationToken cancellationToken)` | `(string epid, string? xEBAYCMARKETPLACEID, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |

`CharityClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCharityOrgAsync` | `(string charityOrgId, string xEbayCMarketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string charityOrgId, string xEBAYCMARKETPLACEID, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetCharityOrgsAsync` | `(string xEbayCMarketplaceId, string? query, RequestOptions? options, CancellationToken cancellationToken)` | `(string xEBAYCMARKETPLACEID, string? limit, string? offset, string? q, string? registrationIds, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |

`MetadataClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCurrenciesAsync` | `(string marketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetItemConditionPoliciesAsync` | `(string marketplaceId, string? categoryId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? filter, string? acceptEncoding, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetReturnPoliciesAsync` | `(string marketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? filter, string? acceptEncoding, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetShippingCarriersAsync` | `(string marketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetShippingPoliciesAsync` | `(string marketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |

`TaxonomyClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCategorySubtreeAsync` | `(string categoryTreeId, string categoryId, RequestOptions? options, CancellationToken cancellationToken)` | `(string categoryId, string categoryTreeId, string? acceptEncoding, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetCategorySuggestionsAsync` | `(string categoryTreeId, string query, RequestOptions? options, CancellationToken cancellationToken)` | `(string categoryTreeId, string q, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetCategoryTreeAsync` | `(string categoryTreeId, RequestOptions? options, CancellationToken cancellationToken)` | `(string categoryTreeId, string? acceptEncoding, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetDefaultCategoryTreeIdAsync` | `(string marketplaceId, RequestOptions? options, CancellationToken cancellationToken)` | `(string marketplaceId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |
| `GetItemAspectsForCategoryAsync` | `(string categoryTreeId, string categoryId, RequestOptions? options, CancellationToken cancellationToken)` | `(string categoryId, string categoryTreeId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)` |

### Added

`CatalogClient`:

- `SearchAsync(string? xEBAYCMARKETPLACEID, string? aspectFilter, string? categoryIds, string? fieldgroups, string? gtin, string? limit, string? mpn, string? offset, string? q, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`

`MetadataClient`:

- `GetAutomotivePartsCompatibilityPoliciesAsync(string marketplaceId, string? filter, string? acceptEncoding, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCategoryPoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetClassifiedAdPoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCompatibilitiesBySpecificationAsync(string xEBAYCMARKETPLACEID, SpecificationRequest body, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCompatibilityPropertyNamesAsync(string xEBAYCMARKETPLACEID, PropertyNamesRequest body, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCompatibilityPropertyValuesAsync(string xEBAYCMARKETPLACEID, PropertyValuesRequest body, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetExcludeShippingLocationsAsync(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetExtendedProducerResponsibilityPoliciesAsync(string marketplaceId, string? filter, string? acceptEncoding, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetHandlingTimesAsync(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetHazardousMaterialsLabelsAsync(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetListingStructurePoliciesAsync(string marketplaceId, string? filter, string? acceptEncoding, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetListingTypePoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetMinimumListingPricePoliciesAsync(string marketplaceId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetMotorsListingPoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetMultiCompatibilityPropertyValuesAsync(string xEBAYCMARKETPLACEID, MultiCompatibilityPropertyValuesRequest body, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetNegotiatedPricePoliciesAsync(string marketplaceId, string? filter, string? acceptEncoding, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetProductCompatibilitiesAsync(string xEBAYCMARKETPLACEID, ProductRequest body, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetProductSafetyLabelsAsync(string marketplaceId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetRegulatoryPoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetSalesTaxJurisdictionsAsync(string countryCode, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetShippingLocationsAsync(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetShippingServicesAsync(string marketplaceId, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetSiteVisibilityPoliciesAsync(string marketplaceId, string? filter, string? acceptLanguage, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`

`TaxonomyClient`:

- `FetchItemAspectsAsync(string categoryTreeId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCompatibilityPropertiesAsync(string categoryTreeId, string categoryId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetCompatibilityPropertyValuesAsync(string categoryTreeId, string compatibilityProperty, string categoryId, string? filter, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
- `GetExpiredCategoriesAsync(string categoryTreeId, string? accessToken, RequestOptions? requestOptions, CancellationToken ct)`
