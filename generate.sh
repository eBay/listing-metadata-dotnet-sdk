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

echo "Pinning Kiota to 1.35.0..."
dotnet tool update microsoft.openapi.kiota --local --version 1.35.0 --allow-downgrade

echo "Restoring tools..."
dotnet tool restore

echo "Regenerating REST clients via Kiota..."
rm -rf ./EBay.ListingMetadata/Generated

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/catalog.json \
  -c CatalogApiClient \
  -n EBay.ListingMetadata.Generated.Catalog \
  -o EBay.ListingMetadata/Generated/Catalog \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/charity.json \
  -c CharityApiClient \
  -n EBay.ListingMetadata.Generated.Charity \
  -o EBay.ListingMetadata/Generated/Charity \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/metadata.json \
  -c MetadataApiClient \
  -n EBay.ListingMetadata.Generated.Metadata \
  -o EBay.ListingMetadata/Generated/Metadata \
  --ebc --co

dotnet kiota generate \
  -l csharp \
  -d specs/openapi/taxonomy.json \
  -c TaxonomyApiClient \
  -n EBay.ListingMetadata.Generated.Taxonomy \
  -o EBay.ListingMetadata/Generated/Taxonomy \
  --ebc --co

echo "Applying Kiota workarounds..."
# Fails when the broken text is gone: Kiota fixed the bug, so remove the workaround from
# api-sdk-definitions src/openapi-distribute/generate-sh/kiota-workarounds.ts.
apply_kiota_workaround() {
  local file="$1" find="$2" replace="$3" reason="$4"
  if ! grep -qF -- "$find" "$file"; then
    echo "Kiota workaround no longer applies: '$find' not found in $file." >&2
    echo "Kiota may have fixed it ($reason); remove the workaround from api-sdk-definitions src/openapi-distribute/generate-sh/kiota-workarounds.ts." >&2
    exit 1
  fi
  FIND="$find" REPLACE="$replace" perl -0pi -e 's/\Q$ENV{FIND}\E/$ENV{REPLACE}/g' "$file"
}
apply_kiota_workaround 'EBay.ListingMetadata/Generated/Metadata/Models/ErrorMessage.cs' 'get => Errors?.MessageEscaped ?? string.Empty;' 'get => (Errors is null ? null : global::System.Linq.Enumerable.FirstOrDefault(Errors))?.MessageEscaped ?? string.Empty;' 'https://github.com/microsoft/kiota/pull/8013'

echo "Adding license headers to generated files..."
./add-license-headers.sh ./EBay.ListingMetadata/Generated

echo "Building (compile check)..."
DOTNET_ROLL_FORWARD=LatestMajor dotnet build EBay.ListingMetadata/EBay.ListingMetadata.csproj

echo "Done."
