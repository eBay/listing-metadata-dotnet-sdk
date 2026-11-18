using EBay.ListingMetadata.Clients.Rest;
using EBay.ListingMetadata.Configuration;
using EBay.ListingMetadata.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EBay.ListingMetadata.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void Registers_one_shared_entry_client_and_no_individual_api_clients()
    {
        var services = new ServiceCollection();
        services.AddListingMetadataSdk(new ListingMetadataConfiguration { AccessToken = "t" });

        using var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<ListingMetadataClient>();

        Assert.Same(client, provider.CreateScope().ServiceProvider.GetRequiredService<ListingMetadataClient>());
        Assert.NotNull(client.Taxonomy);
        Assert.Null(provider.GetService<TaxonomyClient>());
    }

    [Fact]
    public void Rejects_a_null_primary_handler()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ListingMetadataClient(new ListingMetadataConfiguration(), (HttpMessageHandler)null!));
    }
}
