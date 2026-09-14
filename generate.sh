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

echo "Restoring tools..."
dotnet tool restore

echo "Regenerating REST clients via Kiota..."
rm -rf ./EBay.ListingMetadata/Generated

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/catalog.yaml \
  -c CatalogApiClient \
  -n EBay.ListingMetadata.Generated.Catalog \
  -o EBay.ListingMetadata/Generated/Catalog \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/charity.yaml \
  -c CharityApiClient \
  -n EBay.ListingMetadata.Generated.Charity \
  -o EBay.ListingMetadata/Generated/Charity \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/metadata.yaml \
  -c MetadataApiClient \
  -n EBay.ListingMetadata.Generated.Metadata \
  -o EBay.ListingMetadata/Generated/Metadata \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/taxonomy.yaml \
  -c TaxonomyApiClient \
  -n EBay.ListingMetadata.Generated.Taxonomy \
  -o EBay.ListingMetadata/Generated/Taxonomy \
  --ebc --co

echo "Adding license headers to generated files..."
./add-license-headers.sh ./EBay.ListingMetadata/Generated

echo "Building (triggers StrawberryShake GraphQL codegen)..."
DOTNET_ROLL_FORWARD=LatestMajor dotnet build EBay.ListingMetadata/EBay.ListingMetadata.csproj

echo "Done."
