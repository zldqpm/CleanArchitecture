using FluentAssertions;
using Application.Common.Exceptions;
using Application.Devices.Commands.CreateDevice;
using Application.Devices.Commands.DeleteDevice;
using Application.Products.Command.CreateProduct;
using Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Devices.Commands
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class DeleteDeviceTests
    {
        public DeleteDeviceTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidDeviceId()
        {
            var command = new DeleteDeviceCommand
            {
                Id = 69
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should().
                ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldDeleteDevice()
        {
            var productId = await SendAsync(new CreateProductCommand
            {
                Code = "product code",
                Name = "product name",
                Secret = "123qwe"
            });

            var deviceId = await SendAsync(new CreateDeviceCommand
            {
                Code = "device code111",
                Name = "device name111",
                Secret = "234wer111",
                ProductId = productId
            });

            await SendAsync(new DeleteDeviceCommand
            {
                Id = deviceId
            });

            var list = await FindAsync<Device>(productId);

            list.Should().BeNull();
        }
    }
}
