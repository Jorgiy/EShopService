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
        
        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                throw new BuisenessException($"Product with id = {id} not exsists");
            }
            
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetPaginatedProducts(int pageSize, int pageNumber)
        {
            var products = await _productRepository.GetPaginatedProducts(pageSize, pageNumber);
            return products.Select(x => _mapper.Map<Product, ProductDto>(x)).ToList();
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return products.Select(x => _mapper.Map<Product, ProductDto>(x)).ToList();
        }

        public async Task UpdateProductDescription(int id, string description)
        {
            try
            {
                await _productRepository.UpdateDescription(id, description);
            }
            catch (ObjectNotFoundException)
            {
                throw new BuisenessException($"Product with id = {id} not exsists");
            }
        }
    }
}