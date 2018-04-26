using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShopService.Core.CoreServices;
using EShopService.Core.Dto;
using EShopService.Core.Exceptions;
using EShopService.Core.Interfaces;
using EShopService.Data.Mocks;
using EShopService.Data.Models;
using Xunit;
using ValidationException = EShopService.Core.Exceptions.ValidationException;

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
            // act
            var product = await _productCoreService.GetProductById(11);
            
            // assert
            Assert.Null(product);
        }
        
        [Fact]
        public async Task GetProductById_PassNullId_ThrowsValidationException()
        {
            // act, assert
            var exception =
                await Assert.ThrowsAsync<ValidationException>(() => _productCoreService.GetProductById(null));
            Assert.Equal("id", exception.InvalidFieldNames.First());
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassNullArguments_ReturnAllProducts()
        {
            // act
            var products = await _productCoreService.GetPaginatedProducts(null, null);
            
            // assert
            Assert.Equal(10, products.Count);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassPageSize_ReturnProperCountOfProducts()
        {
            // act
            var products = await _productCoreService.GetPaginatedProducts(5, null);
            
            // assert
            Assert.Equal(5, products.Count);
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
        public async Task GetPaginatedProducts_PassOnlyPageNumber_ThrowsValidationException()
        {
            // act, assert
            var exception =
                await Assert.ThrowsAsync<ValidationException>(() => _productCoreService.GetPaginatedProducts(null, 1));
            Assert.Equal("pageSize", exception.InvalidFieldNames.First());
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassNegativePageNumber_ThrowsValidationException()
        {
            // act, assert
            var exception =
                await Assert.ThrowsAsync<ValidationException>(() => _productCoreService.GetPaginatedProducts(1, -1));
            Assert.Equal("pageNumber", exception.InvalidFieldNames.First());
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassExistingId_DescriptionChanged()
        {
            // arrange
            const string newDescription = "newDescription";
            
            // act
            await _productCoreService.UpdateProductDescription(1,newDescription);
            var updatedProduct = await _productCoreService.GetProductById(1);
            
            // assert
            Assert.Equal(newDescription, updatedProduct.Description);
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassNotExsistingId_ThrowsBuisenessEsception()
        {
            // act, assert
            var exception = await Assert.ThrowsAsync<BuisenessException>(() =>
                _productCoreService.UpdateProductDescription(11, "newDescription"));
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassingNullProductId_ThrowsValidationEsception()
        {
            // act, assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _productCoreService.UpdateProductDescription(null, "newDescription"));
            Assert.Equal("id", exception.InvalidFieldNames.First());
        }
    }
}