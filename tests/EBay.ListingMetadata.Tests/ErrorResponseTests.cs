using System.Net;
using EBay.ListingMetadata.Generated.Metadata.Models;
using Xunit;

namespace EBay.ListingMetadata.Tests;

public sealed class ErrorResponseTests
{
    [Fact]
    public async Task Metadata_errors_carry_ebays_first_error_message()
    {
        // ErrorMessage.Message is patched after Kiota generation (api-sdk-definitions kiota-workarounds.ts,
        // microsoft/kiota#8013): Kiota's own code for this collection-backed message does not compile.
        var sdk = new TestSdk
        {
            Handler =
            {
                Status = HttpStatusCode.BadRequest,
                ResponseBody = """{"errors":[{"errorId":45003,"message":"Invalid marketplace."},{"errorId":1,"message":"second"}]}"""
            }
        };

        var error = await Assert.ThrowsAsync<ErrorMessage>(() =>
            sdk.Client.Metadata.GetCompatibilitiesBySpecificationAsync("EBAY_US", new SpecificationRequest()));

        Assert.Equal("Invalid marketplace.", error.Message);
        Assert.Equal(400, error.ResponseStatusCode);
        Assert.Equal(2, error.Errors!.Count);
    }

    [Fact]
    public async Task An_empty_error_list_gives_an_empty_message()
    {
        var sdk = new TestSdk { Handler = { Status = HttpStatusCode.BadRequest, ResponseBody = """{"errors":[]}""" } };

        var error = await Assert.ThrowsAsync<ErrorMessage>(() =>
            sdk.Client.Metadata.GetCompatibilitiesBySpecificationAsync("EBAY_US", new SpecificationRequest()));

        Assert.Equal(string.Empty, error.Message);
    }
}
