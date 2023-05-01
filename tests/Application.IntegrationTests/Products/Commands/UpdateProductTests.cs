using FluentAssertions;
using Application.Common.Exceptions;
using Application.Products.Command.CreateProduct;
using Application.Products.Command.UpdateProduct;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Products.Commands
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class UpdateProductTests
    {
        public UpdateProductTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequiredValidProductId()
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Code = "ele",
                Name = "电梯",
                Secret = "123qwe"
            };

            FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldUpdateProduct()
        {
            var listId = await SendAsync(new CreateProductCommand
            {
                Code = "ele",
                Name = "电梯",
                Secret = "123qwe"
            });

            var command = new UpdateProductCommand
            {
                Id = listId,
                Code = "light",
                Name = "电灯",
                Secret = "qwe123"
            };

            await SendAsync(command);

            var list = await FindAsync<Product>(listId);

            list.Code.Should().Be(command.Code);
            list.Name.Should().Be(command.Name);
            list.Secret.Should().NotBeNull();
        }
    }
}