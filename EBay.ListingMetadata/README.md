# ListingMetadataSdk

Unified .NET 10 SDK for eBay Listing Metadata APIs, providing type-safe clients for REST (Catalog, Taxonomy, Metadata, Charity) and GraphQL (Inventory Mapping) operations.

## Features

- **Type-safe** - Strongly-typed models and clients for REST and GraphQL
- **Unified API** - Single client for all eBay Listing Metadata operations (15 total)
- **REST + GraphQL** - 13 REST operations + 2 GraphQL operations
- **OAuth2 Support** - Built-in token refresh and per-request token override
- **Dependency Injection** - First-class DI support
- **Async/Await** - Modern async patterns throughout
- **.NET 10** - Built for the latest .NET runtime

## Installation

```bash
dotnet add package ListingMetadataSdk
```

## Quick Start

### Basic Usage

```csharp
using ListingMetadataSdk;
using ListingMetadataSdk.Configuration;

var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    GraphQLEndpoint = "https://graphqlapi.ebay.com/graphql",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.Bearer,
        BearerToken = "your-access-token"
    }
};

using var client = new UnifiedClient(config);

// REST: Catalog API - Get product details
var product = await client.Catalog.GetProductAsync("12345678", "EBAY_US");
Console.WriteLine($"Product: {product.Title}");

// REST: Taxonomy API - Get category tree
var categoryTree = await client.Taxonomy.GetDefaultCategoryTreeIdAsync("EBAY_US");
Console.WriteLine($"Category Tree ID: {categoryTree.CategoryTreeId}");

// REST: Metadata API - Get shipping carriers
var carriers = await client.Metadata.GetShippingCarriersAsync("EBAY_US");
foreach (var carrier in carriers.ShippingCarriers ?? [])
{
    Console.WriteLine($"Carrier: {carrier.ShippingCarrierName}");
}

// REST: Charity API - Search charities
var charities = await client.Charity.GetCharityOrgsAsync(
    marketplaceId: "EBAY_US",
    query: "animal",
    limit: 10);
Console.WriteLine($"Found {charities.Total} charities");

// GraphQL: Create listing previews from external products
using ListingMetadataSdk.Models.GraphQL.Inputs;
var createInput = new StartListingPreviewsCreationInput
{
    ExternalProducts = new[]
    {
        new ExternalProductDetailsInput
        {
            Title = "Apple iPhone 15 Pro",
            CategoryName = "Cell Phones & Smartphones",
            Images = new[] { "https://example.com/iphone.jpg" },
            Aspects = new[]
            {
                new ExternalProductDetailsAspectInput
                {
                    Name = "Brand",
                    Values = new[] { "Apple" }
                }
            }
        }
    }
};

var result = await client.GraphQL.StartListingPreviewsCreationAsync(createInput);
Console.WriteLine($"Task ID: {result.ListingPreviewsCreationTask?.Id}");
```

### Dependency Injection

```csharp
using ListingMetadataSdk.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddListingMetadataSdk(config =>
{
    config.BaseUrl = "https://api.ebay.com";
    config.Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenProvider = async (ct) => await GetAccessTokenAsync(ct)
    };
    config.Timeout = TimeSpan.FromSeconds(30);
});

var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<UnifiedClient>();
```

### OAuth2 with Token Refresh

```csharp
// Option 1: Token Provider (simple, called per-request)
var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",
    Authentication = new AuthenticationConfig
    {
        Type = AuthenticationType.OAuth2,
        TokenProvider = async (ct) => 
        {
            // Your logic to get current token
            return await FetchCurrentTokenAsync(ct);
        }
    }
};

// Option 2: Token Refresh Handler (advanced, automatic refresh)
public class MyTokenHandler : ITokenRefreshHandler
{
    private string _accessToken;
    private DateTime _expiresAt;

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _expiresAt)
        {
            await RefreshTokenAsync(cancellationToken);
        }
        return _accessToken;
    }

    public async Task RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        // Your OAuth2 token refresh logic
        var response = await RequestNewTokenAsync(cancellationToken);
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
        TokenRefreshHandler = new MyTokenHandler()
    }
};
```

### Per-Request Token Override

```csharp
// Useful for multi-tenant scenarios
var product = await client.Catalog.GetProductAsync(
    epid: "12345678",
    marketplaceId: "EBAY_US",
    accessToken: userSpecificToken);
```

## API Coverage

### REST APIs (13 operations)

**Catalog API (1 operation)**
- `GetProductAsync` - Retrieve product details by ePID

**Taxonomy API (5 operations)**
- `GetDefaultCategoryTreeIdAsync` - Get default category tree for marketplace
- `GetCategoryTreeAsync` - Get complete category hierarchy
- `GetCategorySuggestionsAsync` - Get category suggestions for a query
- `GetItemAspectsForCategoryAsync` - Get aspects for a specific category
- `GetCategorySubtreeAsync` - Get category subtree

**Metadata API (5 operations)**
- `GetReturnPoliciesAsync` - Get return policy metadata
- `GetShippingPoliciesAsync` - Get shipping policy metadata
- `GetItemConditionPoliciesAsync` - Get item condition policies
- `GetShippingCarriersAsync` - Get available shipping carriers
- `GetCurrenciesAsync` - Get supported currencies

**Charity API (2 operations)**
- `GetCharityOrgAsync` - Get charity organization details
- `GetCharityOrgsAsync` - Search for charity organizations

### GraphQL API (2 operations)

**Inventory Mapping API**
- `StartListingPreviewsCreationAsync` - Create listing previews from external products (title, images, GTINs, aspects)
- `ListingPreviewsCreationTaskByIdAsync` - Get listing preview task status and results

## Configuration Options

```csharp
var config = new SdkConfiguration
{
    BaseUrl = "https://api.ebay.com",           // Required
    Authentication = authConfig,                 // Optional
    Timeout = TimeSpan.FromSeconds(30),         // Default: 30s
    MaxRetries = 3                               // Default: 3
};
```

## Error Handling

```csharp
using ListingMetadataSdk.Exceptions;

try
{
    var product = await client.Catalog.GetProductAsync("invalid-epid");
}
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    Console.WriteLine("Product not found");
}
catch (SdkException ex)
{
    Console.WriteLine($"SDK error: {ex.Message}");
}
```

## Building from Source

```bash
dotnet build
dotnet pack
```

## Generated Operations

This SDK was generated from the following OpenAPI specifications:

- **commerce_catalog_v1_beta_oas3** - Catalog API
- **commerce_taxonomy_v1_oas3** - Taxonomy API
- **sell_metadata_v1_oas3** - Metadata API
- **commerce_charity_v1_oas3** - Charity API

Operations were selected based on `operations.json` configuration.

## License

[Apache 2.0](../LICENSE) © 2026 eBay Inc.

## Support

For issues and questions, please refer to the eBay Developer Program documentation or create an issue in this repository.
