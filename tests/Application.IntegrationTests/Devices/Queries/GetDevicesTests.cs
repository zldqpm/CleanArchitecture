using FluentAssertions;
using Application.Devices.Queries.GetDevices;
using Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Devices.Queries
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class GetDevicesTests
    {
        public GetDevicesTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task ShouldReturnProducts()
        {
            var query = new GetDevicesQuery();
            var result = await SendAsync(query);

            result.Lists.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldReturnAllProductsAndDevices()
        {
            await AddAsync(new Product
            {
                Code = "product code",
                Name = "product name",
                Secret = "123qwe",
                Devices = new List<Device>
                {
                    new()
                    {
                        Code = "device code111",
                        Name = "device name111",
                        Secret = "234wer111" 
                    }
                }
            });

            var query = new GetDevicesQuery();
            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
        }
    }
}
