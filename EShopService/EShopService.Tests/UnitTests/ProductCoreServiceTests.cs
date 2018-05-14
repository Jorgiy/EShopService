using System.Threading.Tasks;
using AutoMapper;
using EShopService.Core.CoreServices;
using EShopService.Core.Dto;
using EShopService.Core.Exceptions;
using EShopService.Core.Interfaces;
using EShopService.Data.Mocks;
using EShopService.Data.Models;
using Xunit;

namespace EShopService.Tests.UnitTests
{
    public class ProductCoreServiceTests
    {
        private readonly IProductCoreService _productCoreService = new ProductCoreService(new ProductRepositoryMock(),
            new MapperConfiguration(x => { x.CreateMap<Product, ProductDto>(); }));

        [Fact]
        public async Task GetProductById_PassExsistingId_ReturnProduct()
        {
            // act
            var product = await _productCoreService.GetProductById(1);

            // assert
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public async Task GetProductById_PassNotExistingId_ReturnNull()
        {
            // act, assert
            await Assert.ThrowsAsync<BuisenessException>(() => _productCoreService.GetProductById(11));
        }
        
        [Fact]
        public async Task GetAllProducts_DefaultCall_ReturnAll()
        {
            // act
            var products = await _productCoreService.GetAllProducts();

            // assert
            Assert.Equal(10, products.Count);
        }

        [Fact]
        public async Task GetPaginatedProducts_PassPageSizeAndPageNumber_ReturnProperCountOfProducts()
        {
            // act
            var products = await _productCoreService.GetPaginatedProducts(3, 4);

            // assert
            Assert.Equal(1, products.Count);
        }

        [Fact]
        public async Task UpdateProductDescription_PassExistingId_DescriptionChanged()
        {
            // arrange
            const string newDescription = "newDescription";

            // act
            await _productCoreService.UpdateProductDescription(1, newDescription);
            var updatedProduct = await _productCoreService.GetProductById(1);

            // assert
            Assert.Equal(newDescription, updatedProduct.Description);
        }

        [Fact]
        public async Task UpdateProductDescription_PassNotExsistingId_ThrowsBuisenessEsception()
        {
            // act, assert
            await Assert.ThrowsAsync<BuisenessException>(() =>
                _productCoreService.UpdateProductDescription(11, "newDescription"));
        }
    }
}