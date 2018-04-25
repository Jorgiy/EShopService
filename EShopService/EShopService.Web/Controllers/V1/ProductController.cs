using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShopService.Core.Dto;
using EShopService.Core.Interfaces;
using EShopService.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EShopService.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class ProductController : Controller
    {
        private readonly IProductCoreService _productCoreService;
        
        private readonly IMapper _mapper;
        
        public ProductController(IProductCoreService productCoreService, MapperConfiguration mapperConfiguration)
        {
            _productCoreService = productCoreService;
            _mapper = mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Get one product dy it's identifier
        /// </summary>
        /// <param name="id">Product's identifier</param>
        /// <returns>Product entity</returns>
        [HttpGet("product/{id}")]
        public async Task<ProductViewModel> GetProduct(int? id)
        {
            var product = await _productCoreService.GetProductById(id);
            return _mapper.Map<ProductDto, ProductViewModel>(product);
        }

        /// <summary>
        /// Get paginated products
        /// </summary>
        /// <param name="request">Count of products to return and number of portion to return (can not be defined if page size is not)</param>
        /// <returns>Array of products</returns>
        [HttpGet("products")]
        public async Task<ProductViewModel[]> GetPaginatedProducts([FromQuery]GetPaginatedProductsViewModel request)
        {
            var products = await _productCoreService.GetPaginatedProducts(request.PageSize, request.PageNumber);
            return products.Select(x => _mapper.Map<ProductDto, ProductViewModel>(x)).ToArray();
        }

        /// <summary>
        /// Update particuar product's description
        /// </summary>
        /// <param name="request">Product identifier and New description</param>
        /// <returns></returns>
        [HttpPatch("update")]
        public async Task UpdateProductDescription([FromBody]UpdateProductDescriptionViewModel request)
        {
            await _productCoreService.UpdateProductDescription(request.Id, request.Description);
        }
    }
}