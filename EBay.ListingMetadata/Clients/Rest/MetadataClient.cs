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

using EBay.ListingMetadata.Configuration;
using EBay.ListingMetadata.Generated.Metadata.Models;
using EBay.ListingMetadata.Generated.Metadata;
using Microsoft.Kiota.Abstractions;

namespace EBay.ListingMetadata.Clients.Rest;

public sealed class MetadataClient
{
    private readonly MetadataApiClient _kiota;
    private readonly ListingMetadataConfiguration _config;
    private readonly ITokenContext _tokenContext;
    private readonly IRequestAdapter _adapter;

    internal MetadataClient(MetadataApiClient kiota, ListingMetadataConfiguration config, ITokenContext tokenContext, IRequestAdapter adapter)
    {
        _kiota = kiota;
        _config = config;
        _tokenContext = tokenContext;
        _adapter = adapter;
    }

    /// <summary>
    /// This method returns the eBay policies that define how to list automotive parts compatibility items in the categories of the specified marketplace.
    /// By default, this method returns all categories that support parts compatibility. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the category IDs you want to review.
    /// <b>Note: </b>To return policy information for the eBay US marketplace, specify <c>EBAY_MOTORS_US</c> as the path parameter for <b>marketplace_id</b>.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a <b>204 No content</b> status code is returned with an empty response body.
    /// </summary>
    public Task<AutomotivePartsCompatibilityPolicyResponse?> GetAutomotivePartsCompatibilityPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_automotive_parts_compatibility_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay category policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// </summary>
    public Task<CategoryPolicyResponse?> GetCategoryPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_category_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay classified ad policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// <b>Note: </b>This method does not support classified ads for eBay US Motors categories (EBAY_MOTORS_US). For eBay Motors Pro users, use <see href="/api-docs/sell/metadata/resources/marketplace/methods/getMotorsListingPolicies">getMotorsListingPolicies</see>.
    /// </summary>
    public Task<ClassifiedAdPolicyResponse?> GetClassifiedAdPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_classified_ad_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns the default currency used by the eBay marketplace specified in the request. This is the currency that the seller should use when providing price data for this marketplace through listing APIs.
    /// </summary>
    public Task<GetCurrenciesResponse?> GetCurrenciesAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_currencies.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns the Extended Producer Responsibility policies for one, multiple, or all eBay categories in an eBay marketplace.
    /// The identifier of the eBay marketplace is passed in as a path parameter, and unless one or more eBay category IDs are passed in through the filter query parameter, this method will return metadata on every applicable category for the specified marketplace.
    /// <b>Note:</b> Currently, the Extended Producer Responsibility policies are only applicable to a limited number of categories.
    /// <b>Note: </b>Extended Producer Responsibility IDs are no longer set at the listing level so category-level metadata is no longer returned. Instead, sellers will provide/manage these IDs at the account level by going to <see href="https://accountsettings.ebay.fr/epr-fr">Account Settings</see>.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<ExtendedProducerResponsibilityPolicyResponse?> GetExtendedProducerResponsibilityPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_extended_producer_responsibility_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns hazardous materials label information for the specified eBay marketplace. The information includes IDs, descriptions, and URLs (as applicable) for the available signal words, statements, and pictograms. The returned statements are localized for the default language of the marketplace. If a marketplace does not support hazardous materials label information, no response payload is returned, but only a <b>204 No content</b> status code.
    /// This information is used by the seller to add hazardous materials label related information to their listings (see <see href="/api-docs/sell/static/metadata/feature-regulatorhazmatcontainer.html">Specifying hazardous material related information</see>).
    /// </summary>
    public Task<HazardousMaterialDetailsResponse?> GetHazardousMaterialsLabelsAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_hazardous_materials_labels.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns item condition metadata on one, multiple, or all eBay categories on an eBay marketplace. This metadata consists of the different item conditions (with IDs) that an eBay category supports, and a boolean to indicate if an eBay category requires an item condition.
    /// If applicable, this metadata also shows the different condition descriptors (with IDs) that an eBay category supports.
    /// <b>Note:</b> Currently, condition grading is only applicable to the following trading card leaf categories:
    /// - Non-Sport Trading Card Singles
    /// - CCG Individual Cards
    /// - Sports Trading Cards Singles
    /// and the following coin categories:
    /// - Coins: US
    /// - Coins: World
    /// - Coins: Canada
    /// - Coins: Ancient
    /// - Coins: Medieval
    /// Note that these coin categories are not leaf categories, so condition grading is available for all leaf categories descending from the above categories (except for rolls, sets, and lots).
    /// Additionally, condition grading is available for Salvage items in select categories for eligible users. Use the <b>getItemConditionPolicies</b> method to see supported categories.
    /// The identifier of the eBay marketplace is passed in as a path parameter, and unless one or more eBay category IDs are passed in through the <b>filter</b> query parameter, this method will return metadata on every single category for the specified marketplace. If you only want to view item condition metadata for one eBay category or a select group of eBay categories, you can pass in up to 50 eBay category ID through the <b>filter</b> query parameter.
    /// <b>Important:</b> <b>Certified - Refurbished</b>-eligible sellers, and sellers who are eligible to list with the new values (EXCELLENT_REFURBISHED, VERY_GOOD_REFURBISHED, and GOOD_REFURBISHED) must use an OAuth token created with the <see href="/api-docs/static/oauth-authorization-code-grant.html">authorization code grant flow</see> and <b>https://api.ebay.com/oauth/api_scope/sell.inventory</b> scope in order to retrieve the refurbished conditions for the relevant categories.
    /// Refurbished item conditions are only supported in the Australia, Canada, French Canada, Germany, France, Italy, UK, and US marketplaces. See the <see href="https://www.ebay.com/sellercenter/ebay-for-business/ebay-refurbished-program">eBay Refurbished Program</see> page in help center for the categories that support refurbished conditions.
    /// These restricted item conditions will not be returned if an OAuth token created with the <see href="/api-docs/static/oauth-client-credentials-grant.html">client credentials grant flow</see> and <b>https://api.ebay.com/oauth/api_scope</b> scope is used, or if any seller is not eligible to list with that item condition.
    /// See the <see href="/api-docs/static/oauth-scopes.html">Specifying OAuth scopes</see> topic for more information about specifying scopes.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<ItemConditionPolicyResponse?> GetItemConditionPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_item_condition_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns the eBay policies that define the allowed listing structures for the categories of a specific marketplace. The listing-structure policies currently pertain to whether or not you can list items with variations.
    /// By default, this method returns the entire category tree for the specified marketplace. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the category IDs you want to review.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<ListingStructurePolicyResponse?> GetListingStructurePoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_listing_structure_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay listing type policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// </summary>
    public Task<ListingTypePoliciesResponse?> GetListingTypePoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_listing_type_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns minimum listing price policies for supported types of listings on a specific marketplace. This includes the minimum start price for auction listings, the minimum sale price for fixed-price listings, and the minimum percentage value that a Buy It Now or auction listing must be above the minimum start price for the same listing.
    /// <b>Note:</b> The only applicable listing type values for this method are <c>AUCTION</c> and <c>FIXED_PRICE_ITEM</c>.
    /// The identifier of the eBay marketplace for which to retrieve supported minimum listing price policies is passed through the <b>marketplace_id</b> path parameter.
    /// </summary>
    public Task<GetMinimumListingPricePoliciesResponse?> GetMinimumListingPricePoliciesAsync(
        string marketplaceId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_minimum_listing_price_policies.GetAsync(cfg =>
        {
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay Motors policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// <b>Note:</b> To return policy information for eBay US Motors categories, specify <b>marketplace_id</b> as <c>EBAY_MOTORS_US</c>.
    /// </summary>
    public Task<MotorsListingPoliciesResponse?> GetMotorsListingPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_motors_listing_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns the eBay policies that define the supported negotiated price features (like "best offer") for the categories of a specific marketplace.
    /// By default, this method returns the entire category tree for the specified marketplace. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the category IDs you want to review.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<NegotiatedPricePolicyResponse?> GetNegotiatedPricePoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_negotiated_price_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns product safety label information for the specified eBay marketplace. The information includes IDs, descriptions, and URLs (as applicable) for the available statements and pictograms. The returned statements are localized for the default language of the marketplace. If a marketplace does not support product safety label information, no response payload is returned, but only a <b>204 No content</b> status code.
    /// This information is used by the seller to add product safety label related information to their listings. The <see href="/develop/api/sell/metadata_api#sell-metadata_api-marketplace-getregulatorypolicies">getRegulatoryPolicies</see> method can be used to see which categories recommend or require product safety labels.
    /// </summary>
    public Task<ProductSafetyLabelsResponse?> GetProductSafetyLabelsAsync(
        string marketplaceId,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_product_safety_labels.GetAsync(cfg =>
        {
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns regulatory policies for one, multiple, or all eBay categories in an eBay marketplace. The identifier of the eBay marketplace is passed in as a path parameter, and unless one or more eBay category IDs are passed in through the filter query parameter, this method will return metadata for every listing category in the specified marketplace.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<RegulatoryPolicyResponse?> GetRegulatoryPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_regulatory_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns the eBay policies that define whether or not you must include a return policy for the items you list in the categories of a specific marketplace, plus the guidelines for creating domestic and international return policies in the different eBay categories.
    /// By default, this method returns the entire category tree for the specified marketplace. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the category IDs you want to review.
    /// <b>Tip:</b> This method can potentially return a very large response payload. eBay recommends that the response payload be compressed by passing in the <b>Accept-Encoding</b> request header and setting the value to <c>gzip</c>.
    /// </summary>
    public Task<ReturnPolicyResponse?> GetReturnPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptEncoding = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_return_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptEncoding is not null)
            {
                cfg.Headers.TryAdd("Accept-Encoding", acceptEncoding);
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay shipping policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// </summary>
    public Task<ShippingPoliciesResponse?> GetShippingPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_shipping_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method returns eBay international site visibility policy metadata for all leaf categories on the specified marketplace.
    /// By default, this method returns metadata on all leaf categories. You can limit the size of the result set by using the <b>filter</b> query parameter to specify only the leaf category IDs you want to review.
    /// If you specify a valid marketplace ID but that marketplace does not contain policy information, or if you filter out all results, a successful call returns a <b>204 No content</b> status code with an empty response body.
    /// </summary>
    public Task<SiteVisibilityPoliciesResponse?> GetSiteVisibilityPoliciesAsync(
        string marketplaceId,
        string? filter = null,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Marketplace[marketplaceId].Get_site_visibility_policies.GetAsync(cfg =>
        {
            if (filter is not null)
            {
                cfg.QueryParameters.Filter = filter;
            }
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method is used to retrieve all compatible application name-value pairs for a part based on the provided specification(s).
    /// The part's relevant dimensions and/or characteristics can be provided through the <b>specifications</b> container. For example, when retrieving compatible application name-value pairs for a tire, the tire's dimensions (such as the section width or rim diameter) should be provided.
    /// By default, all compatible application name-value pairs for the specifications are returned. You can limit the size of the result set by using the <b>compatibilityPropertyFilters</b> array to specify the properties (such as make, model, year, or trim) you wish to be included in the response.
    /// <b>Note:</b> The <see href="/develop/api/sell/metadata_api#sell-metadata_api-compatibilities-getcompatibilitypropertynames">getCompatibilityPropertyNames</see> and <see href="/develop/api/sell/metadata_api#sell-metadata_api-compatibilities-getcompatibilitypropertyvalues">getCompatibilityPropertyValues</see> methods can be used to retrieve valid property names and values that can be used as the name-value pairs to define specifications.
    /// </summary>
    public Task<SpecificationResponse?> GetCompatibilitiesBySpecificationAsync(
        string xEBAYCMARKETPLACEID,
        SpecificationRequest body,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Compatibilities.Get_compatibilities_by_specification.PostAsync(body, cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method is used to retrieve product compatibility property names for the specified compatibility-enabled category.
    /// Compatibility property names can be used alongside the corresponding compatibility property value (retrieved using the <see href="/develop/api/sell/metadata_api#sell-metadata_api-compatibilities-getcompatibilitypropertyvalues">getCompatibilityPropertyValues</see> method) to describe the assembly for which an item is compatible.
    /// The <b>categoryId</b> of the compatibility-enabled category for which to retrieve compatibility property names is required in the request body.
    /// By default, all property names within the compatibility category of the specified compatibility-enable category are returned. You can limit the size of the result set by using the <b>dataset</b> array to specify the types of properties you want returned.
    /// </summary>
    public Task<PropertyNamesResponse?> GetCompatibilityPropertyNamesAsync(
        string xEBAYCMARKETPLACEID,
        PropertyNamesRequest body,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Compatibilities.Get_compatibility_property_names.PostAsync(body, cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method is used to retrieve product compatibility property values associated with a single property name, in the specified category.
    /// Compatibility property values can be used alongside the corresponding compatibility property name (retrieved using the <see href="/develop/api/sell/metadata_api#sell-metadata_api-compatibilities-getcompatibilitypropertynames">getCompatibilityPropertyNames</see> method) to describe the assembly for which an item is compatible.
    /// The <b>categoryId</b> of the compatibility-enabled category for which to retrieve compatibility property values is required in the request body, as well as the <b>propertyName</b> for which you wish to retrieve associated values.
    /// By default, all property values associated with the specified <b>propertyName</b> are returned. You can limit the size of the result set by using the <b>propertyFilter</b> array. Only property values associated with the specified name-value pairs will be returned.
    /// </summary>
    public Task<PropertyValuesResponse?> GetCompatibilityPropertyValuesAsync(
        string xEBAYCMARKETPLACEID,
        PropertyValuesRequest body,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Compatibilities.Get_compatibility_property_values.PostAsync(body, cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method is used to retrieve product compatibility property values associated with multiple property names, in the specified category.
    /// Compatibility property values can be used alongside the corresponding compatibility property name (retrieved using the <see href="/develop/api/sell/metadata_api#sell-metadata_api-compatibilities-getcompatibilitypropertynames">getCompatibilityPropertyNames</see> method) to describe the assembly for which an item is compatible.
    /// The <b>categoryId</b> of the compatibility-enabled category for which to retrieve compatibility property values is required in the request body, as well as the <b>propertyNames</b> for which you wish to retrieve associated property values. The <b>propertyFilter</b> array is also required to constrain the output. Only property values associated with the specified name-value pairs will be returned.
    /// </summary>
    public Task<MultiCompatibilityPropertyValuesResponse?> GetMultiCompatibilityPropertyValuesAsync(
        string xEBAYCMARKETPLACEID,
        MultiCompatibilityPropertyValuesRequest body,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Compatibilities.Get_multi_compatibility_property_values.PostAsync(body, cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method is used to retrieve all available item compatibility details for the specified product.
    /// Item compatibility details can be used to see the properties for which an item is compatible. For example, if you are searching for a part for a specific vehicle, you can use this method to see the years, engine, and/or trim for which the part is compatible. Item compatibility details are returned as name-value pairs.
    /// The product for which to retrieve item compatibility details must be provided through the <b>productIdentifier</b> field. This value can be either an eBay specific identifier (such as an ePID) or an external identifier (such as a UPC).
    /// By default, all available item compatibility details for the specified product are returned. You can limit the size of the result set using the <b>dataset</b> or <b>datasetPropertyName</b> fields to specify the types of properties you want returned in the response. The <b>applicationPropertyFilter</b> array can also be used so that only parts compatible with the specified name-value pairs are returned.
    /// </summary>
    public Task<ProductResponse?> GetProductCompatibilitiesAsync(
        string xEBAYCMARKETPLACEID,
        ProductRequest body,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Compatibilities.Get_product_compatibilities.PostAsync(body, cfg =>
        {
            cfg.Headers.TryAdd("X-EBAY-C-MARKETPLACE-ID", xEBAYCMARKETPLACEID);
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves a list of locations that the seller can use as excluded shipping locations within their listings or in their fulfillment business policies for the specified marketplace. These are locations that a seller designates as areas where they will not ship items.
    /// Excluded shipping locations and ship-to locations are used in tandem at the listing level and in fulfillment business policies. Excluded shipping locations and ship-to locations share a lot of the same values and they should not contradict each other.
    /// Manage excluded shipping locations using business policies through the <b>fulfillment_policy</b> resource of the <b>Account v1 API</b>.
    /// </summary>
    public Task<ShippingExcludeLocationResponse?> GetExcludeShippingLocationsAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Shipping.Marketplace[marketplaceId].Get_exclude_shipping_locations.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves a list of supported handling times for the specified marketplace. The handling time returned specifies the maximum number of business days the eBay site allows for shipping an item to domestic buyers after receiving a cleared payment. Handling times apply to both domestic and international orders. If the handling time is 1 day, the seller commits to dropping the item off for shipment one business day after payment clears.
    /// Manage handing times using business policies through the <b>fulfillment_policy</b> resource of the <b>Account v1 API</b>.
    /// </summary>
    public Task<ShippingHandlingTimeResponse?> GetHandlingTimesAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Shipping.Marketplace[marketplaceId].Get_handling_times.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves a list of supported shipping carriers for the specified marketplace. It provides essential information for sellers to understand which shipping carriers are available for use when listing items on that eBay marketplace. Knowing the supported carriers can help sellers optimize their shipping options and ensure efficient delivery to buyers.
    /// The value returned in the <b>shippingCarrier</b> field is the enumerated value required when providing shipment tracking information for that carrier.
    /// <b>Tip: </b> Use the <see href="/develop/api/sell/metadata_api#sell-metadata_api-shipping:marketplace-getshippingservices">getShippingServices</see> method to explore available shipping services for each carrier.
    /// Manage shipping carriers using business policies through the <b>fulfillment_policy</b> resource of the <b>Account v1 API</b>.
    /// </summary>
    public Task<ShippingCarrierResponse?> GetShippingCarriersAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Shipping.Marketplace[marketplaceId].Get_shipping_carriers.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves a list of supported shipping locations for the specified marketplace. It provides sellers with information on where they can ship their items. Sellers can use this information to configure their shipping settings.
    /// <b>Tip: </b> Use the <see href="/develop/api/sell/metadata_api#sell-metadata_api-shipping:marketplace-getexcludeshippinglocations">getExcludeShippingLocations</see> method to return locations where the seller does not ship.
    /// Manage shipping locations using business policies through the <b>fulfillment_policy</b> resource of the <b>Account v1 API</b>.
    /// </summary>
    public Task<ShippingLocationResponse?> GetShippingLocationsAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Shipping.Marketplace[marketplaceId].Get_shipping_locations.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves a list of shipping services supported for the specified marketplace, including valid shipping services, shipping times, and package constraints such as size and weight.
    /// Manage shipping services using business policies through the <b>fulfillment_policy</b> resource of the <b>Account v1 API</b>.
    /// </summary>
    public Task<ShippingServiceResponse?> GetShippingServicesAsync(
        string marketplaceId,
        string? acceptLanguage = null,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Shipping.Marketplace[marketplaceId].Get_shipping_services.GetAsync(cfg =>
        {
            if (acceptLanguage is not null)
            {
                cfg.Headers.TryAdd("Accept-Language", acceptLanguage);
            }
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));

    /// <summary>
    /// This method retrieves all sales-tax jurisdictions for the country specified in the <b>countryCode</b> path parameter. Countries with valid sales-tax jurisdictions are Canada and the US.
    /// The response from this call tells you the jurisdictions for which a seller can configure tax tables. Although setting up tax tables is optional, you can use the <b>createOrReplaceSalesTax</b> method in the <b>Account API</b> call to configure the tax tables for the jurisdictions into which you sell.
    /// <b>Note:</b> Sales-tax tables are only available for the US (EBAY_US) and Canada (EBAY_CA) marketplaces.
    /// Important! </b>"&gt;<b>Important!</b> In the US, eBay now calculates, collects, and remits sales tax to the proper taxing authorities in all 50 states and Washington, DC. Sellers can no longer specify sales-tax rates for these jurisdictions using a tax table.
    /// However, sellers may continue to use a sales-tax table to set rates for the following US territories:
    /// - American Samoa (AS)
    /// - Guam (GU)
    /// - Northern Mariana Islands (MP)
    /// - Palau (PW)
    /// - US Virgin Islands (VI)
    /// For additional information, refer to <see href="https://www.ebay.com/help/selling/fees-credits-invoices/taxes-import-charges?id=4121">Taxes and import charges</see>.
    /// </summary>
    public Task<SalesTaxJurisdictions?> GetSalesTaxJurisdictionsAsync(
        string countryCode,
        string? accessToken = null,
        RequestOptions? requestOptions = null,
        CancellationToken ct = default)
        => RestInvocation.CallAsync(
            _tokenContext,
            accessToken,
            () => _kiota.Country[countryCode].Sales_tax_jurisdiction.GetAsync(cfg =>
        {
            RestInvocation.ApplyHeaders(cfg.Headers, _config, requestOptions);
        }, ct));
}
