# ChangeLog

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
