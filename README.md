# Listing Metadata SDK

A unified .NET SDK for eBay Listing Metadata APIs, providing type-safe access to Catalog, Taxonomy, Charity, Metadata, and GraphQL APIs.

## 🚨 IMPORTANT: Dual Token Architecture

This SDK requires **TWO SEPARATE authentication tokens**:

| Token Type | APIs | Required Scopes |
|------------|------|-----------------|
| **REST Token** | Catalog, Taxonomy, Metadata, Charity | `api_scope`, `sell.inventory` |
| **GraphQL Token** | GraphQL Listing Preview | `sell.inventory.mapping` |

**Why?** eBay uses different endpoints (`api.ebay.com` vs `graphqlapi.ebay.com`) with different OAuth scopes and permissions. You must configure both tokens separately.

## Features

- ✅ **4 REST APIs** - Catalog, Taxonomy, Metadata, Charity (40+ operations)
- ✅ **GraphQL API** - Listing Preview / Inventory Mapping
- ✅ **Separate Authentication** - Independent tokens for REST vs GraphQL
- ✅ **Auto-Refresh** - OAuth2 with automatic 2-hour token renewal
- ✅ **Type-Safe** - Kiota-generated clients from OpenAPI specs
- ✅ **Dependency Injection** - First-class ASP.NET Core DI support
- ✅ **Modern .NET** - Targets .NET 8, .NET 9, and .NET 10 with nullable reference types
- ✅ **Async/Await** - All operations are fully asynchronous

## Installation

```bash
dotnet add package ListingMetadataSdk
```

Or add to your `.csproj`:

```xml
<PackageReference Include="ListingMetadataSdk" Version="0.2.0" />
```

## Quick Start

### Basic Usage

```csharp
using ListingMetadataSdk;
using ListingMetadataSdk.Configuration;

var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        BearerToken = "your-access-token"
    }
};

using var client = new UnifiedClient(config);

// Search for products in the catalog
var searchResults = await client.Catalog.SearchAsync(
    q: "iPhone 15",
    categoryIds: "9355",
    limit: 10
);

// Get a specific product by ePID
var product = await client.Catalog.GetProductAsync("12345");

// Get charity organization details
var charity = await client.Charity.GetCharityOrgAsync("charity-id");

// Get category tree
var categoryTree = await client.Taxonomy.GetCategoryTreeAsync("0");

// Get marketplace policies
var policies = await client.Metadata.GetReturnPoliciesAsync("EBAY_US");
```

### Dependency Injection (ASP.NET Core)

In `Program.cs` or `Startup.cs`:

```csharp
using ListingMetadataSdk.Extensions;

builder.Services.AddListingMetadataSdk(config =>
{
    config.BaseUrl = "https://api.ebay.com";
    config.Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenProvider = async (ct) => await GetAccessTokenAsync(ct)
    };
});
```

Then inject in your services or controllers:

```csharp
public class ProductService
{
    private readonly UnifiedClient _client;

    public ProductService(UnifiedClient client)
    {
        _client = client;
    }

    public async Task<Product> GetProductAsync(string epid)
    {
        return await _client.Catalog.GetProductAsync(epid);
    }
}
```

## API Clients

The SDK provides four specialized clients accessible through the `UnifiedClient`:

### 1. Catalog API (`client.Catalog`)

Search and retrieve eBay catalog products:

- `GetProductAsync(epid)` - Get product details by ePID
- `SearchAsync(...)` - Search products with various filters

### 2. Charity API (`client.Charity`)

Access charitable organization information:

- `GetCharityOrgAsync(charityOrgId)` - Get specific charity details
- `GetCharityOrgsAsync(...)` - Search for charities

### 3. Taxonomy API (`client.Taxonomy`)

Category and aspect information:

- `FetchItemAspectsAsync(categoryTreeId)` - Get aspects for all leaf categories
- `GetCategoryTreeAsync(categoryTreeId)` - Get category tree details
- `GetCategorySubtreeAsync(...)` - Get category subtree
- `GetCategorySuggestionsAsync(...)` - Get category suggestions
- `GetItemAspectsForCategoryAsync(...)` - Get aspects for a category
- `GetCompatibilityPropertiesAsync(...)` - Get compatibility properties
- `GetCompatibilityPropertyValuesAsync(...)` - Get compatibility values
- `GetExpiredCategoriesAsync(...)` - Get expired categories
- `GetDefaultCategoryTreeIdAsync(marketplaceId)` - Get default category tree

### 4. Metadata API (`client.Metadata`)

Marketplace policies and configuration (27 operations):

**Policy Operations:**
- `GetAutomotivePartsCompatibilityPoliciesAsync(marketplaceId)`
- `GetCategoryPoliciesAsync(marketplaceId)`
- `GetClassifiedAdPoliciesAsync(marketplaceId)`
- `GetExtendedProducerResponsibilityPoliciesAsync(marketplaceId)`
- `GetHazardousMaterialsLabelsAsync(marketplaceId)`
- `GetItemConditionPoliciesAsync(marketplaceId)`
- `GetListingStructurePoliciesAsync(marketplaceId)`
- `GetListingTypePoliciesAsync(marketplaceId)`
- `GetMotorsListingPoliciesAsync(marketplaceId)`
- `GetNegotiatedPricePoliciesAsync(marketplaceId)`
- `GetProductSafetyLabelsAsync(marketplaceId)`
- `GetRegulatoryPoliciesAsync(marketplaceId)`
- `GetReturnPoliciesAsync(marketplaceId)`
- `GetShippingPoliciesAsync(marketplaceId)`
- `GetSiteVisibilityPoliciesAsync(marketplaceId)`
- `GetCurrenciesAsync(marketplaceId)`

**Compatibility Operations:**
- `GetCompatibilitiesBySpecificationAsync(requestBody)`
- `GetCompatibilityPropertyNamesAsync(requestBody)`
- `GetCompatibilityPropertyValuesAsync(requestBody)`
- `GetMultiCompatibilityPropertyValuesAsync(requestBody)`
- `GetProductCompatibilitiesAsync(requestBody)`

**Shipping Operations:**
- `GetExcludeShippingLocationsAsync(marketplaceId)`
- `GetHandlingTimesAsync(marketplaceId)`
- `GetShippingCarriersAsync(marketplaceId)`
- `GetShippingLocationsAsync(marketplaceId)`
- `GetShippingServicesAsync(marketplaceId)`
- `GetSalesTaxJurisdictionsAsync(countryCode)`

## Authentication

The SDK supports multiple authentication methods:

### Bearer Token

```csharp
var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.Bearer,
        BearerToken = "your-token"
    }
};
```

### OAuth 2.0 with Token Provider

For dynamic token retrieval:

```csharp
var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenProvider = async (ct) => await GetCurrentTokenAsync(ct)
    }
};
```

### OAuth 2.0 with Automatic Refresh

Implement `ITokenRefreshHandler` for automatic token refresh:

```csharp
public class EbayTokenHandler : ITokenRefreshHandler
{
    private string _accessToken;
    private DateTime _expiresAt;

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (DateTime.UtcNow >= _expiresAt)
        {
            await RefreshTokenAsync(cancellationToken);
        }
        return _accessToken;
    }

    public async Task RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        // Call eBay OAuth token endpoint
        var response = await GetNewTokenFromEbayAsync();
        _accessToken = response.AccessToken;
        _expiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
    }
}

var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenRefreshHandler = new EbayTokenHandler()
    }
};
```

### Per-Request Token Override

Override the token for individual requests:

```csharp
var product = await client.Catalog.GetProductAsync(
    epid: "12345",
    accessToken: "user-specific-token"
);
```

## Error Handling

```csharp
using ListingMetadataSdk.Exceptions;

try
{
    var product = await client.Catalog.GetProductAsync("invalid-epid");
}
catch (ApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
    Console.WriteLine($"Response: {ex.ResponseContent}");
}
catch (SdkException ex)
{
    Console.WriteLine($"SDK Error: {ex.Message}");
}
```

## Advanced Configuration

```csharp
var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenRefreshHandler = tokenHandler
    },
    Timeout = TimeSpan.FromSeconds(60), // Custom timeout
    MaxRetries = 5 // Maximum retry attempts
};
```

## Building the SDK

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests (if available)
dotnet test

# Create NuGet package
dotnet pack --configuration Release
```

## Project Structure

```
ListingMetadataSdk/
├── Configuration/
│   ├── SdkConfiguration.cs
│   └── AuthenticationConfig.cs
├── Models/
│   ├── Catalog/
│   │   └── Responses/
│   │       └── Product.cs
│   ├── Charity/
│   │   └── Responses/
│   │       └── CharityOrg.cs
│   ├── Taxonomy/
│   ├── Metadata/
│   └── Common/
│       └── Error.cs
├── Clients/
│   ├── BaseClient.cs
│   ├── UnifiedClient.cs
│   └── Rest/
│       ├── CatalogClient.cs
│       ├── CharityClient.cs
│       ├── TaxonomyClient.cs
│       └── MetadataClient.cs
├── Exceptions/
│   └── SdkException.cs
└── Extensions/
    └── ServiceCollectionExtensions.cs
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

[Apache 2.0](LICENSE) © 2026 eBay Inc.

## Generated Operations Summary

This SDK includes all operations from the listing metadata catalog:

- **Catalog API**: 2 operations (getProduct, search)
- **Charity API**: 2 operations (getCharityOrg, getCharityOrgs)
- **Taxonomy API**: 9 operations (category tree and aspect operations)
- **Metadata API**: 27 operations (policies, compatibility, shipping)

**Total**: 40+ operations for comprehensive eBay listing metadata management

## Requirements

- .NET 8.0, .NET 9.0, or .NET 10.0
- Valid eBay API credentials
- OAuth 2.0 access token for authentication

## Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/yourusername/listing-metadata-dotnet-sdk).

---

🤖 Generated with [Claude Code](https://claude.com/claude-code)
