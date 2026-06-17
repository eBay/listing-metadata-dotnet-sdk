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

namespace EBay.ListingMetadata.Models.Taxonomy.Responses;

/// <summary>
/// Basic category tree identification
/// </summary>
public sealed record BaseCategoryTree
{
    /// <summary>
    /// Unique identifier of the eBay category tree
    /// </summary>
    [JsonPropertyName("categoryTreeId")]
    public string? CategoryTreeId { get; init; }

    /// <summary>
    /// Version of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeVersion")]
    public string? CategoryTreeVersion { get; init; }
}

/// <summary>
/// Complete category tree with hierarchy
/// </summary>
public sealed record CategoryTree
{
    /// <summary>
    /// List of eBay marketplace identifiers using this tree
    /// </summary>
    [JsonPropertyName("applicableMarketplaceIds")]
    public List<string>? ApplicableMarketplaceIds { get; init; }

    /// <summary>
    /// Unique identifier of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeId")]
    public string? CategoryTreeId { get; init; }

    /// <summary>
    /// Version of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeVersion")]
    public string? CategoryTreeVersion { get; init; }

    /// <summary>
    /// Root node containing all category hierarchy
    /// </summary>
    [JsonPropertyName("rootCategoryNode")]
    public CategoryTreeNode? RootCategoryNode { get; init; }
}

/// <summary>
/// Category tree node (recursive structure)
/// </summary>
public sealed record CategoryTreeNode
{
    /// <summary>
    /// Category identifier
    /// </summary>
    [JsonPropertyName("category")]
    public Category? Category { get; init; }

    /// <summary>
    /// Unique identifier of the parent of this node
    /// </summary>
    [JsonPropertyName("parentCategoryTreeNodeAncestorHref")]
    public string? ParentCategoryTreeNodeAncestorHref { get; init; }

    /// <summary>
    /// Array of child nodes (subcategories)
    /// </summary>
    [JsonPropertyName("childCategoryTreeNodes")]
    public List<CategoryTreeNode>? ChildCategoryTreeNodes { get; init; }

    /// <summary>
    /// Indicates whether this is a leaf category
    /// </summary>
    [JsonPropertyName("categoryTreeNodeLevel")]
    public int? CategoryTreeNodeLevel { get; init; }

    /// <summary>
    /// Indicates if this category is a leaf node
    /// </summary>
    [JsonPropertyName("leafCategoryTreeNode")]
    public bool? LeafCategoryTreeNode { get; init; }
}

/// <summary>
/// Category information
/// </summary>
public sealed record Category
{
    /// <summary>
    /// Unique eBay-assigned category identifier
    /// </summary>
    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; init; }

    /// <summary>
    /// Display name of the category
    /// </summary>
    [JsonPropertyName("categoryName")]
    public string? CategoryName { get; init; }
}

/// <summary>
/// Category subtree response
/// </summary>
public sealed record CategorySubtree
{
    /// <summary>
    /// Subtree hierarchy node
    /// </summary>
    [JsonPropertyName("categorySubtreeNode")]
    public CategoryTreeNode? CategorySubtreeNode { get; init; }

    /// <summary>
    /// Unique identifier of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeId")]
    public string? CategoryTreeId { get; init; }

    /// <summary>
    /// Version of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeVersion")]
    public string? CategoryTreeVersion { get; init; }
}

/// <summary>
/// Category suggestion response
/// </summary>
public sealed record CategorySuggestionResponse
{
    /// <summary>
    /// Suggested categories sorted by relevance
    /// </summary>
    [JsonPropertyName("categorySuggestions")]
    public List<CategorySuggestion>? CategorySuggestions { get; init; }

    /// <summary>
    /// Unique identifier of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeId")]
    public string? CategoryTreeId { get; init; }

    /// <summary>
    /// Version of the category tree
    /// </summary>
    [JsonPropertyName("categoryTreeVersion")]
    public string? CategoryTreeVersion { get; init; }
}

/// <summary>
/// Category suggestion
/// </summary>
public sealed record CategorySuggestion
{
    /// <summary>
    /// Category information
    /// </summary>
    [JsonPropertyName("category")]
    public Category? Category { get; init; }

    /// <summary>
    /// Tree path from root to this category
    /// </summary>
    [JsonPropertyName("categoryTreeNodeAncestors")]
    public List<AncestorReference>? CategoryTreeNodeAncestors { get; init; }

    /// <summary>
    /// Relevance indicator for this suggestion
    /// </summary>
    [JsonPropertyName("categoryTreeNodeLevel")]
    public int? CategoryTreeNodeLevel { get; init; }

    /// <summary>
    /// Relevance score for the suggestion
    /// </summary>
    [JsonPropertyName("relevancy")]
    public string? Relevancy { get; init; }
}

/// <summary>
/// Ancestor category reference
/// </summary>
public sealed record AncestorReference
{
    /// <summary>
    /// Category identifier
    /// </summary>
    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; init; }

    /// <summary>
    /// Category name
    /// </summary>
    [JsonPropertyName("categoryName")]
    public string? CategoryName { get; init; }

    /// <summary>
    /// Subtree node level
    /// </summary>
    [JsonPropertyName("categorySubtreeNodeHref")]
    public string? CategorySubtreeNodeHref { get; init; }

    /// <summary>
    /// Tree node level
    /// </summary>
    [JsonPropertyName("categoryTreeNodeLevel")]
    public int? CategoryTreeNodeLevel { get; init; }
}

/// <summary>
/// Aspect metadata response
/// </summary>
public sealed record AspectMetadata
{
    /// <summary>
    /// List of item aspects appropriate for the category
    /// </summary>
    [JsonPropertyName("aspects")]
    public List<AspectDetail>? Aspects { get; init; }
}

/// <summary>
/// Detailed aspect information
/// </summary>
public sealed record AspectDetail
{
    /// <summary>
    /// Localized name of the aspect
    /// </summary>
    [JsonPropertyName("localizedAspectName")]
    public string? LocalizedAspectName { get; init; }

    /// <summary>
    /// Constraints and requirements for this aspect
    /// </summary>
    [JsonPropertyName("aspectConstraint")]
    public AspectConstraint? AspectConstraint { get; init; }

    /// <summary>
    /// Values suggested for this aspect
    /// </summary>
    [JsonPropertyName("aspectValues")]
    public List<AspectValue>? AspectValues { get; init; }

    /// <summary>
    /// Relevance indicator for this aspect
    /// </summary>
    [JsonPropertyName("relevanceIndicator")]
    public RelevanceIndicator? RelevanceIndicator { get; init; }
}

/// <summary>
/// Aspect constraint information
/// </summary>
public sealed record AspectConstraint
{
    /// <summary>
    /// Data type of the aspect
    /// </summary>
    [JsonPropertyName("aspectDataType")]
    public string? AspectDataType { get; init; }

    /// <summary>
    /// Format of the aspect value
    /// </summary>
    [JsonPropertyName("aspectFormat")]
    public string? AspectFormat { get; init; }

    /// <summary>
    /// Indicates if multi-value is enabled
    /// </summary>
    [JsonPropertyName("aspectEnabledForVariations")]
    public bool? AspectEnabledForVariations { get; init; }

    /// <summary>
    /// Indicates if aspect is required
    /// </summary>
    [JsonPropertyName("aspectRequired")]
    public bool? AspectRequired { get; init; }

    /// <summary>
    /// Mode for aspect (e.g., SELECTION_ONLY, FREE_TEXT)
    /// </summary>
    [JsonPropertyName("aspectMode")]
    public string? AspectMode { get; init; }

    /// <summary>
    /// Maximum number of values allowed
    /// </summary>
    [JsonPropertyName("itemToAspectCardinality")]
    public string? ItemToAspectCardinality { get; init; }
}

/// <summary>
/// Aspect value with localized content
/// </summary>
public sealed record AspectValue
{
    /// <summary>
    /// Localized value
    /// </summary>
    [JsonPropertyName("localizedValue")]
    public string? LocalizedValue { get; init; }

    /// <summary>
    /// Value constraints
    /// </summary>
    [JsonPropertyName("valueConstraints")]
    public List<ValueConstraint>? ValueConstraints { get; init; }
}

/// <summary>
/// Value constraint
/// </summary>
public sealed record ValueConstraint
{
    /// <summary>
    /// Applicable marketplace
    /// </summary>
    [JsonPropertyName("applicableForLocalizedAspectName")]
    public string? ApplicableForLocalizedAspectName { get; init; }

    /// <summary>
    /// Applicable aspect value
    /// </summary>
    [JsonPropertyName("applicableForLocalizedAspectValue")]
    public string? ApplicableForLocalizedAspectValue { get; init; }
}

/// <summary>
/// Relevance indicator
/// </summary>
public sealed record RelevanceIndicator
{
    /// <summary>
    /// Search boost level
    /// </summary>
    [JsonPropertyName("searchBoost")]
    public int? SearchBoost { get; init; }
}
