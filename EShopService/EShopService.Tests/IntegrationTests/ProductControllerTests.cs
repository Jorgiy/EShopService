using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace EShopService.Tests.IntegrationTests
{
    public class ProductControllerTests
    {
        private readonly TestContext _testContext;

        public ProductControllerTests()
        {
            // arrange
            _testContext = new TestContext(); 
        }

        [Fact]
        public async Task GetProduct_PassExistingId_OK()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/product/1");

            // assert
            result.EnsureSuccessStatusCode();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GetProduct_PassNotExistingId_NoContent()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/product/11");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task GetProduct_PassNullArgument_BadRequest()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/product/id=");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassOnlyPageSize_OK()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/paginated-products?PageSize=1");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassBothArguments_OK()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/paginated-products?PageSize=1&PageNumber=1");

            // assert
            result.EnsureSuccessStatusCode();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassOnlyPageNumber_BadRequest()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/paginated-products?PageNumber=1");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassNegativePageNumber_BadRequest()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/paginated-products?PageSize=3&PageNumber=-1");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetPaginatedProducts_PassNegativePageSize_BadRequest()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/paginated-products?PageSize=-3");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetAllProducts_DefaultQuery_OK()
        {
            // act
            var result = await _testContext.Client.GetAsync("api/v1.0/all-products");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassExistingId_OK()
        {
            //arrange
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, "api/v1.0/update")
            {
                Content = new StringContent("{\"id\": 1,\"description\": \"newData\"}", 
                    Encoding.UTF8, "application/json-patch+json")
            };
            
            // act
            var result = await _testContext.Client.SendAsync(request);

            // assert
            result.EnsureSuccessStatusCode();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassNotExsistingProductId_UnprocessableEntity()
        {
            //arrange
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, "api/v1.0/update")
            {
                Content = new StringContent("{\"id\": 11,\"description\": \"newData\"}", 
                    Encoding.UTF8, "application/json-patch+json")
            };
            
            // act
            var result = await _testContext.Client.SendAsync(request);

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task UpdateProductDescription_PassNotNullProductId_BadRequest()
        {
            //arrange
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, "api/v1.0/update")
            {
                Content = new StringContent("{\"description\": \"newData\"}", 
                    Encoding.UTF8, "application/json-patch+json")
            };
            
            // act
            var result = await _testContext.Client.SendAsync(request);

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}