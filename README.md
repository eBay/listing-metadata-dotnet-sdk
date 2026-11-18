# eBay Listing Metadata SDK for .NET

Look up the catalog, categories, item aspects, marketplace listing policies and charities you need to create good listings. This SDK wraps the eBay Catalog API, Charity API, Metadata API and Taxonomy API behind one client, `ListingMetadataClient`.

- **Target frameworks:** .NET Framework 4.7.2, .NET 8, .NET 9 and .NET 10
- **C#:** usable from C# 7.3 and later

## Installation

The package `EBay.ListingMetadata` is not on nuget.org yet. Reference the project, or build the package and add it to a local feed:

```bash
dotnet pack EBay.ListingMetadata -c Release -o artifacts/packages
```

## Quick start

```csharp
using EBay.ListingMetadata;
using EBay.ListingMetadata.Configuration;

var config = new ListingMetadataConfiguration
{
    // A user or application access token from your own OAuth flow (see "Authentication").
    AccessToken = Environment.GetEnvironmentVariable("EBAY_ACCESS_TOKEN")
};

using var client = new ListingMetadataClient(config);

var tree = await client.Taxonomy.GetDefaultCategoryTreeIdAsync("EBAY_US");
Console.WriteLine($"Category tree {tree?.CategoryTreeId}");
```

## APIs

| `ListingMetadataClient` property | API | Default base URL | Access token |
|---|---|---|---|
| `Catalog` | Catalog API | `https://api.ebay.com/commerce/catalog/v1_beta` | User |
| `Charity` | Charity API | `https://api.ebay.com/commerce/charity/v1` | Application or user |
| `Metadata` | Metadata API | `https://api.ebay.com/sell/metadata/v1` | Application or user |
| `Taxonomy` | Taxonomy API | `https://api.ebay.com/commerce/taxonomy/v1` | Application or user |

"Access token" is what eBay's OpenAPI spec declares: an application token (client credentials) or a user token (authorization code). Scopes are listed per API below.

<details>
<summary><b>Catalog</b>: Catalog API, 2 operations</summary>

Scopes: `https://api.ebay.com/oauth/api_scope/commerce.catalog.readonly`, `https://api.ebay.com/oauth/api_scope/sell.inventory`

> **Note:** This method requires that certain special characters in the query parameters be percent-encoded:

| Method | Description |
|---|---|
| `GetProductAsync` | This method retrieves details of the catalog product identified by the eBay product identifier (ePID) specified in the request. |
| `SearchAsync` | This method searches for and retrieves summaries of one or more products in the eBay catalog that match the search criteria provided by a seller. |

</details>

<details>
<summary><b>Charity</b>: Charity API, 2 operations</summary>

Scopes: `https://api.ebay.com/oauth/api_scope`

| Method | Description |
|---|---|
| `GetCharityOrgAsync` | This call is used to retrieve detailed information about supported charitable organizations. |
| `GetCharityOrgsAsync` | This call is used to search for supported charitable organizations. |

</details>

<details>
<summary><b>Metadata</b>: Metadata API, 28 operations</summary>

Scopes: `https://api.ebay.com/oauth/api_scope`, `https://api.ebay.com/oauth/api_scope/sell.inventory`

| Method | Description |
|---|---|
| `GetAutomotivePartsCompatibilityPoliciesAsync` | This method returns the eBay policies that define how to list automotive parts compatibility items in the categories of the specified marketplace. |
| `GetCategoryPoliciesAsync` | This method returns eBay category policy metadata for all leaf categories on the specified marketplace. |
| `GetClassifiedAdPoliciesAsync` | This method returns eBay classified ad policy metadata for all leaf categories on the specified marketplace. |
| `GetCurrenciesAsync` | This method returns the default currency used by the eBay marketplace specified in the request. |
| `GetExtendedProducerResponsibilityPoliciesAsync` | This method returns the Extended Producer Responsibility policies for one, multiple, or all eBay categories in an eBay marketplace. |
| `GetHazardousMaterialsLabelsAsync` | This method returns hazardous materials label information for the specified eBay marketplace. |
| `GetItemConditionPoliciesAsync` | This method returns item condition metadata on one, multiple, or all eBay categories on an eBay marketplace. |
| `GetListingStructurePoliciesAsync` | This method returns the eBay policies that define the allowed listing structures for the categories of a specific marketplace. |
| `GetListingTypePoliciesAsync` | This method returns eBay listing type policy metadata for all leaf categories on the specified marketplace. |
| `GetMinimumListingPricePoliciesAsync` | This method returns minimum listing price policies for supported types of listings on a specific marketplace. |
| `GetMotorsListingPoliciesAsync` | This method returns eBay Motors policy metadata for all leaf categories on the specified marketplace. |
| `GetNegotiatedPricePoliciesAsync` | This method returns the eBay policies that define the supported negotiated price features (like "best offer") for the categories of a specific marketplace. |
| `GetProductSafetyLabelsAsync` | This method returns product safety label information for the specified eBay marketplace. |
| `GetRegulatoryPoliciesAsync` | This method returns regulatory policies for one, multiple, or all eBay categories in an eBay marketplace. |
| `GetReturnPoliciesAsync` | This method returns the eBay policies that define whether or not you must include a return policy for the items you list in the categories of a specific marketplace, plus the guidelines for creating domestic and internat |
| `GetShippingPoliciesAsync` | This method returns eBay shipping policy metadata for all leaf categories on the specified marketplace. |
| `GetSiteVisibilityPoliciesAsync` | This method returns eBay international site visibility policy metadata for all leaf categories on the specified marketplace. |
| `GetCompatibilitiesBySpecificationAsync` | This method is used to retrieve all compatible application name-value pairs for a part based on the provided specification(s). |
| `GetCompatibilityPropertyNamesAsync` | This method is used to retrieve product compatibility property names for the specified compatibility-enabled category. |
| `GetCompatibilityPropertyValuesAsync` | This method is used to retrieve product compatibility property values associated with a single property name, in the specified category. |
| `GetMultiCompatibilityPropertyValuesAsync` | This method is used to retrieve product compatibility property values associated with multiple property names, in the specified category. |
| `GetProductCompatibilitiesAsync` | This method is used to retrieve all available item compatibility details for the specified product. |
| `GetExcludeShippingLocationsAsync` | This method retrieves a list of locations that the seller can use as excluded shipping locations within their listings or in their fulfillment business policies for the specified marketplace. |
| `GetHandlingTimesAsync` | This method retrieves a list of supported handling times for the specified marketplace. |
| `GetShippingCarriersAsync` | This method retrieves a list of supported shipping carriers for the specified marketplace. |
| `GetShippingLocationsAsync` | This method retrieves a list of supported shipping locations for the specified marketplace. |
| `GetShippingServicesAsync` | This method retrieves a list of shipping services supported for the specified marketplace, including valid shipping services, shipping times, and package constraints such as size and weight. |
| `GetSalesTaxJurisdictionsAsync` | This method retrieves all sales-tax jurisdictions for the country specified in the countryCode path parameter. |

</details>

<details>
<summary><b>Taxonomy</b>: Taxonomy API, 9 operations</summary>

Scopes: `https://api.ebay.com/oauth/api_scope`, `https://api.ebay.com/oauth/api_scope/metadata.insights`

| Method | Description |
|---|---|
| `FetchItemAspectsAsync` | Get item aspects for all leaf categories in a marketplace |
| `GetDefaultCategoryTreeIdAsync` | Get the default category tree ID for a marketplace |
| `GetCategoryTreeAsync` | Get a category tree |
| `GetCategorySubtreeAsync` | Get a category subtree |
| `GetCategorySuggestionsAsync` | Get suggested categories for a keyword |
| `GetItemAspectsForCategoryAsync` | Get aspects for a leaf category |
| `GetCompatibilityPropertiesAsync` | Get compatibility properties for a compatibility-enabled category |
| `GetCompatibilityPropertyValuesAsync` | Get compatibility property values for a compatibility-enabled category |
| `GetExpiredCategoriesAsync` | Get expired categories for a marketplace |

</details>

## Authentication

The SDK does not obtain or refresh tokens: get them with your own OAuth flow ([eBay OAuth guide](https://developer.ebay.com/develop/guides/sell/authorization)) and give them to the SDK in one of two ways:

- `ListingMetadataConfiguration.AccessToken`: used for every call that doesn't pass its own token.
- The `accessToken` parameter on every method: used for that call only, for example a user token where the configuration holds an application token.

```csharp
string userToken = Environment.GetEnvironmentVariable("EBAY_USER_TOKEN")!;
var tree = await client.Taxonomy.GetDefaultCategoryTreeIdAsync("EBAY_US", accessToken: userToken);
```

A token is only ever sent to trusted hosts, see below.

## Configuration

| Property | Type | Default | Description |
|---|---|---|---|
| `CatalogBaseUrl` | `string` | `https://api.ebay.com/commerce/catalog/v1_beta` | Base URL of the Catalog API. |
| `CharityBaseUrl` | `string` | `https://api.ebay.com/commerce/charity/v1` | Base URL of the Charity API. |
| `MetadataBaseUrl` | `string` | `https://api.ebay.com/sell/metadata/v1` | Base URL of the Metadata API. |
| `TaxonomyBaseUrl` | `string` | `https://api.ebay.com/commerce/taxonomy/v1` | Base URL of the Taxonomy API. |
| `AccessToken` | `string?` |  | Fallback bearer token used when no per-call accessToken is supplied. |
| `DiagnosticLogging` | `bool` | `false` | When true, logs the HTTP method, URL, status code, and response reason phrase for all non-success responses to stderr. |
| `HttpTimeout` | `TimeSpan` | 30 seconds | Timeout applied to all HTTP requests. |
| `DefaultRequestOptions` | `RequestOptions` | no headers | Default headers applied to every REST request. |
| `AllowedHosts` | `IList<string>` | empty | Additional hosts trusted with the access token, for example a proxy (see below). |

`ListingMetadataConfiguration.DefaultAllowedHosts` lists the hosts trusted by default.

## Sandbox and trusted hosts

The access token is only sent to trusted hosts: the hosts in `ListingMetadataConfiguration.DefaultAllowedHosts`, which are the eBay production hosts and their `*.sandbox.ebay.com` counterparts, plus any in `AllowedHosts`. Switching to the sandbox needs only the base URLs:

```csharp
var sandboxConfig = new ListingMetadataConfiguration
{
    AccessToken = Environment.GetEnvironmentVariable("EBAY_SANDBOX_ACCESS_TOKEN"),
    CatalogBaseUrl = "https://api.sandbox.ebay.com/commerce/catalog/v1_beta",
    CharityBaseUrl = "https://api.sandbox.ebay.com/commerce/charity/v1",
    MetadataBaseUrl = "https://api.sandbox.ebay.com/sell/metadata/v1",
    TaxonomyBaseUrl = "https://api.sandbox.ebay.com/commerce/taxonomy/v1",
};
```

Any other host, for example a proxy that forwards to eBay, has to be added to `AllowedHosts`; the client constructor throws `InvalidOperationException` otherwise:

```csharp
var proxied = new ListingMetadataConfiguration
{
    CatalogBaseUrl = "https://ebay-proxy.example.com/commerce/catalog/v1_beta",
    AllowedHosts = { "ebay-proxy.example.com" },
};
```

## Dependency injection

`AddListingMetadataSdk` registers the configuration and one shared `ListingMetadataClient` as a singleton. It owns its HTTP connections and is disposed with the container. Inject the client and use its properties for the individual APIs:

```csharp
using EBay.ListingMetadata;
using EBay.ListingMetadata.Configuration;
using EBay.ListingMetadata.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddListingMetadataSdk(new ListingMetadataConfiguration
{
    AccessToken = builder.Configuration["EBay:AccessToken"]
});

var app = builder.Build();
app.MapGet("/example", async (ListingMetadataClient client) =>
{
    var tree = await client.Taxonomy.GetDefaultCategoryTreeIdAsync("EBAY_US");
    return Results.Ok();
});
app.Run();
```

## Custom HTTP handler

To send requests through your own handler, for example for a proxy, client certificates or a test double, pass it to the constructor. The handler is shared by all APIs and is not disposed by the client:

```csharp
var handler = new HttpClientHandler { Proxy = new WebProxy("http://corporate-proxy:8080") };
using var proxiedClient = new ListingMetadataClient(config, handler);
```

## Error handling

```csharp
try
{
    var tree = await client.Taxonomy.GetDefaultCategoryTreeIdAsync("EBAY_US");
}
catch (UnauthorizedException)
{
    // HTTP 401: the token is missing, expired, for the other environment, or lacks a scope.
}
catch (ApiException ex)
{
    // Any other REST error.
    Console.WriteLine($"HTTP {ex.ResponseStatusCode}");
}
```

- `UnauthorizedException` (HTTP 401) derives from `SdkException`.
- Other REST errors throw Kiota's `ApiException`, or a generated subclass named in eBay's spec for that operation, with `ResponseStatusCode`. eBay's error text is only available where the spec describes the error body correctly; set `DiagnosticLogging = true` to log failed requests to stderr.

## .NET Framework

The `net472` build is a classic package that `packages.config` projects can use. Such projects must list every dependency themselves. At the minimum versions the SDK supports, these are:

| Package | Version |
|---|---|
| Microsoft.Kiota.Abstractions, .Bundle, .Http.HttpClientLibrary, .Serialization.Form, .Serialization.Json, .Serialization.Multipart, .Serialization.Text | 1.22.0 |
| Std.UriTemplate | 2.0.8 |
| Microsoft.IO.RecyclableMemoryStream | 3.0.1 |
| Microsoft.Extensions.DependencyInjection.Abstractions | 6.0.0 |
| System.Diagnostics.DiagnosticSource, System.Net.Http.WinHttpHandler | 6.0.0 |
| System.Text.Json, System.Text.Encodings.Web, System.IO.Pipelines, Microsoft.Bcl.AsyncInterfaces | 10.0.0 |
| System.Runtime.CompilerServices.Unsafe | 6.1.2 |
| System.Memory, System.Threading.Tasks.Extensions | 4.6.3 |
| System.Buffers, System.Numerics.Vectors, System.ValueTuple | 4.6.1 |

With these versions the app needs binding redirects for Microsoft.Bcl.AsyncInterfaces (10.0.0.0), System.Buffers (4.0.5.0), System.Memory (4.0.5.0), System.Runtime.CompilerServices.Unsafe (6.0.3.0), System.Text.Encodings.Web (10.0.0.0), System.Text.Json (10.0.0.0) and System.Threading.Tasks.Extensions (4.2.4.0). There must be no redirect for System.ValueTuple: .NET Framework 4.7.1+ provides it. Newer package versions work too and change the redirect versions.

## Development

- .NET 10 SDK, plus the .NET 8 and .NET 9 SDKs so every target framework builds. `net472` builds on any OS; its tests run on Windows only.
- Build: `dotnet build EBay.ListingMetadata`
- Test: `dotnet test tests/EBay.ListingMetadata.Tests`
- Most of this repository is generated from eBay's API specs by the SDK generator (`api-sdk-definitions`): the API clients, `Configuration/`, `Extensions/`, `ListingMetadataClient.cs`, `specs/` and `generate.sh`. Change the generator, not the generated files. `./generate.sh` reruns Kiota for the specs in `specs/openapi`.

See [CONTRIBUTING.adoc](CONTRIBUTING.adoc).

## License

Apache 2.0, see [LICENSE](LICENSE).
