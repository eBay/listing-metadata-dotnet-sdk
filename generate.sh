#!/usr/bin/env bash

# Copyright 2026 eBay Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

set -euo pipefail

echo "Restoring dotnet tools..."
dotnet tool restore

echo "Regenerating REST clients with Kiota..."
dotnet kiota generate \
  -l csharp \
  -d specs/"commerce_catalog_v1_beta_oas3 (2).yaml" \
  -c CatalogApiClient \
  -n EBay.ListingMetadata.Generated.Catalog \
  -o EBay.ListingMetadata/Generated/Catalog \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/"commerce_taxonomy_v1_oas3 (1).yaml" \
  -c TaxonomyApiClient \
  -n EBay.ListingMetadata.Generated.Taxonomy \
  -o EBay.ListingMetadata/Generated/Taxonomy \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/sell_metadata_v1_oas3.yaml \
  -c MetadataApiClient \
  -n EBay.ListingMetadata.Generated.Metadata \
  -o EBay.ListingMetadata/Generated/Metadata \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/commerce_charity_v1_oas3.yaml \
  -c CharityApiClient \
  -n EBay.ListingMetadata.Generated.Charity \
  -o EBay.ListingMetadata/Generated/Charity \
  --ebc --co

echo "Building SDK..."
dotnet build EBay.ListingMetadata/EBay.ListingMetadata.csproj

echo "Done! SDK regenerated successfully."
