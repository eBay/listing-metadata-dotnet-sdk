# ChangeLog

## 0.2.0 (09/14/2026)

### Breaking changes

#### Changed signatures

`CatalogClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetProductAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, RequestOptions?, CancellationToken)` |

`CharityClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCharityOrgAsync` | `(string, string, RequestOptions?, CancellationToken)` | `(string, string, string?, RequestOptions?, CancellationToken)` |
| `GetCharityOrgsAsync` | `(string, string?, RequestOptions?, CancellationToken)` | `(string, string?, string?, string?, string?, string?, RequestOptions?, CancellationToken)` |

`MetadataClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCurrenciesAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, RequestOptions?, CancellationToken)` |
| `GetItemConditionPoliciesAsync` | `(string, string?, RequestOptions?, CancellationToken)` | `(string, string?, string?, string?, string?, RequestOptions?, CancellationToken)` |
| `GetReturnPoliciesAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, string?, string?, RequestOptions?, CancellationToken)` |
| `GetShippingCarriersAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, RequestOptions?, CancellationToken)` |
| `GetShippingPoliciesAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, string?, RequestOptions?, CancellationToken)` |

`TaxonomyClient`:

| Operation | Previous parameters | Current parameters |
| --- | --- | --- |
| `GetCategorySubtreeAsync` | `(string, string, RequestOptions?, CancellationToken)` | `(string, string, string?, string?, RequestOptions?, CancellationToken)` |
| `GetCategorySuggestionsAsync` | `(string, string, RequestOptions?, CancellationToken)` | `(string, string, string?, RequestOptions?, CancellationToken)` |
| `GetCategoryTreeAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, string?, RequestOptions?, CancellationToken)` |
| `GetDefaultCategoryTreeIdAsync` | `(string, RequestOptions?, CancellationToken)` | `(string, string?, RequestOptions?, CancellationToken)` |
| `GetItemAspectsForCategoryAsync` | `(string, string, RequestOptions?, CancellationToken)` | `(string, string, string?, RequestOptions?, CancellationToken)` |

### Added

`CatalogClient`:

- `SearchAsync(string?, string?, string?, string?, string?, string?, string?, string?, string?, string?, RequestOptions?, CancellationToken)`

`MetadataClient`:

- `GetAutomotivePartsCompatibilityPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetCategoryPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetClassifiedAdPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetCompatibilitiesBySpecificationAsync(string, SpecificationRequest, string?, RequestOptions?, CancellationToken)`
- `GetCompatibilityPropertyNamesAsync(string, PropertyNamesRequest, string?, RequestOptions?, CancellationToken)`
- `GetCompatibilityPropertyValuesAsync(string, PropertyValuesRequest, string?, RequestOptions?, CancellationToken)`
- `GetExcludeShippingLocationsAsync(string, string?, string?, RequestOptions?, CancellationToken)`
- `GetExtendedProducerResponsibilityPoliciesAsync(string, string?, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetHandlingTimesAsync(string, string?, string?, RequestOptions?, CancellationToken)`
- `GetHazardousMaterialsLabelsAsync(string, string?, string?, RequestOptions?, CancellationToken)`
- `GetListingStructurePoliciesAsync(string, string?, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetListingTypePoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetMinimumListingPricePoliciesAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetMotorsListingPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetMultiCompatibilityPropertyValuesAsync(string, MultiCompatibilityPropertyValuesRequest, string?, RequestOptions?, CancellationToken)`
- `GetNegotiatedPricePoliciesAsync(string, string?, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetProductCompatibilitiesAsync(string, ProductRequest, string?, RequestOptions?, CancellationToken)`
- `GetProductSafetyLabelsAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetRegulatoryPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`
- `GetSalesTaxJurisdictionsAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetShippingLocationsAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetShippingServicesAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetSiteVisibilityPoliciesAsync(string, string?, string?, string?, RequestOptions?, CancellationToken)`

`TaxonomyClient`:

- `FetchItemAspectsAsync(string, string?, RequestOptions?, CancellationToken)`
- `GetCompatibilityPropertiesAsync(string, string, string?, RequestOptions?, CancellationToken)`
- `GetCompatibilityPropertyValuesAsync(string, string, string, string?, string?, RequestOptions?, CancellationToken)`
- `GetExpiredCategoriesAsync(string, string?, RequestOptions?, CancellationToken)`
