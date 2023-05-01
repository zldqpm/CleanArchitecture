using FluentAssertions;
using Application.Common.Exceptions;
using Application.Products.Command.CreateProduct;
using Application.Products.Command.DeleteProduct;
using Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Products.Commands
{

    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class DeleteProductTests
    {
        public DeleteProductTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidProductId()
        {
            var command = new DeleteProductCommand
            {
                Id = 333
            };

            FluentActions.Invoking(() => SendAsync(command))
                    .Should()
                    .ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldDeleteProducts()
        {
            var listId = await SendAsync(new CreateProductCommand
            {
                Code = "ele",
                Name = "电梯",
                Secret = "123qwe"
            });

            await SendAsync(new DeleteProductCommand
            {
                Id = listId
            });

            var list = await FindAsync<Product>(listId);

            list.Should().BeNull();
        }
    }
}
