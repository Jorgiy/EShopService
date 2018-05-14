using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EShopService.Core.Dto;
using EShopService.Core.Interfaces;
using EShopService.Web.Exceptions;
using EShopService.Web.Middleware;
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
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ExceptionMiddleware.BuisenessErrorResponse), (int)HttpStatusCode.NotFound)]
        [Logging]
        public async Task<ProductViewModel> GetProduct(int id)
        {
            ValidateProductId(id);
            var product = await _productCoreService.GetProductById(id);
            return _mapper.Map<ProductDto, ProductViewModel>(product);
        }

        /// <summary>
        /// Get paginated products
        /// </summary>
        /// <param name="request">Count of products to return and number of portion to return</param>
        /// <returns>All paginated products</returns>
        [HttpGet("paginated-products")]
        [ProducesResponseType(typeof(ProductViewModel[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [Logging]
        public async Task<List<ProductViewModel>> GetPaginatedProducts([FromQuery]GetPaginatedProductsViewModel request)
        {
            ValidatePaginationInput(request.PageNumber, request.PageSize);
            var products = await _productCoreService.GetPaginatedProducts(request.PageSize, request.PageNumber);
            return products.Select(x => _mapper.Map<ProductDto, ProductViewModel>(x)).ToList();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>All products</returns>
        [HttpGet("all-products")]
        [ProducesResponseType(typeof(ProductViewModel[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [Logging]
        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var products = await _productCoreService.GetAllProducts();
            return products.Select(x => _mapper.Map<ProductDto, ProductViewModel>(x)).ToList();
        }

        /// <summary>
        /// Update particuar product's description
        /// </summary>
        /// <param name="request">Product identifier and New description</param>
        /// <returns></returns>
        [HttpPatch("update")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ExceptionMiddleware.BuisenessErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ExceptionMiddleware.ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [Logging]
        public async Task UpdateProductDescription([FromBody]UpdateProductDescriptionViewModel request)
        {
            ValidateProductId(request.Id);
            await _productCoreService.UpdateProductDescription(request.Id, request.Description);
        }

        private void ValidateProductId(int id)
        {
            if (id == 0)
            {
                throw new ValidationException("Product identifier can not be zero")
                {
                    InvalidFieldNames = new List<string> {nameof(id)}
                };
            }
        }

        private void ValidatePaginationInput(int? pageNumber, int? pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ValidationException("Page number can not be zero or negative")
                {
                    InvalidFieldNames = new List<string> {nameof(pageNumber)}
                };
            }
            
            if (pageSize <= 0)
            {
                throw new ValidationException("Page size can not be zero or negative")
                {
                    InvalidFieldNames = new List<string> {nameof(pageNumber)}
                };
            }
        }
    }
}