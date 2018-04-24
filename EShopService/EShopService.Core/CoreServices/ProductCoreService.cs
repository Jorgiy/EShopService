using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShopService.Core.Dto;
using EShopService.Core.Exceptions;
using EShopService.Core.Interfaces;
using EShopService.Data.Interfaces;
using EShopService.Data.Models;

namespace EShopService.Core.CoreServices
{
    public class ProductCoreService : IProductCoreService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;
        
        public ProductCoreService(IProductRepository productRepository, MapperConfiguration mapperConfiguration)
        {
            _productRepository = productRepository;
            _mapper = mapperConfiguration.CreateMapper();
        }
        
        public async Task<ProductDto> GetProductById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("Product identyfier can not be null");
            }
            
            var product = await _productRepository.GetById(id.Value);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetPaginatedProducts(int? pageSize, int? pageNumber)
        {
            List<Product> products;
            if (!pageSize.HasValue && !pageNumber.HasValue)
            {
                products = await _productRepository.GetAllProducts();
                return products.Select(x => _mapper.Map<Product, ProductDto>(x)).ToList();
            }

            if (pageNumber.HasValue && !pageSize.HasValue)
            {
                throw new ValidationException("It can not be page number with no size");
            }
            
            products = await _productRepository.GetPaginatedProducts(pageSize.Value, pageNumber ?? 1);
            return products.Select(x => _mapper.Map<Product, ProductDto>(x)).ToList();
        }

        public async Task UpdateProductDescription(int? id, string description)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("Product identyfier can not be null");
            }

            try
            {
                await _productRepository.UpdateDescription(id.Value, description);
            }
            catch (ObjectNotFoundException)
            {
                throw new BuisenessException($"Product with id = {id.Value} not exsists");
            }
        }
    }
}