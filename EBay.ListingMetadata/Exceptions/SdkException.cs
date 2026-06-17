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


namespace EBay.ListingMetadata.Exceptions;

/// <summary>
/// Base exception for SDK errors
/// </summary>
public class SdkException : Exception
{
    /// <summary>
    /// Initializes a new instance of SdkException
    /// </summary>
    /// <param name="message">Exception message</param>
    public SdkException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of SdkException with inner exception
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public SdkException(string message, Exception innerException)
        : base(message, innerException) { }
}

/// <summary>
/// Thrown when the API returns HTTP 401 Unauthorized.
/// </summary>
public sealed class UnauthorizedException : SdkException
{
    /// <summary>Initializes a new instance of UnauthorizedException.</summary>
    public UnauthorizedException()
        : base("Access token is invalid or expired. Obtain a new token and retry.") { }

    /// <summary>Initializes a new instance of UnauthorizedException with inner exception.</summary>
    /// <param name="inner">Inner exception</param>
    public UnauthorizedException(Exception inner)
        : base("Access token is invalid or expired. Obtain a new token and retry.", inner) { }
}
