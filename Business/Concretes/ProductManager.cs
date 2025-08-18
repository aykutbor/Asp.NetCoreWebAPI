using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Product;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;
        IMapper _mapper;

        public ProductManager(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductForAddDto dto)
        {

            if (dto.UnitPrice < 0)
            {
                throw new BusinessException("Ürün fiyatı 0'dan küçük olamaz.");
            }

            Product? productWithSameName = await _productRepository.GetAsync(p => p.Name == dto.Name);
            if (productWithSameName is not null)
                throw new System.Exception("Aynı isimde 2. ürün eklenemez.");

            // Manuel Mapping
            //Product product = new();
            //product.Name = dto.Name;
            //product.Stock = dto.Stock;
            //product.UnitPrice = dto.UnitPrice;
            //product.CategoryId = dto.CategoryId;
            //product.CreatedDate = DateTime.Now; // Global'e taşınacak

            // AutoMapping

            Product product = _mapper.Map<Product>(dto);  // Product -> Maplemek istenilen tür, dto -> Verilerin transfer edileceği kaynak
            await _productRepository.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

        public void Delete(int id)
        {
            Product? productToDelete = _productRepository.Get(i => i.Id == id);
            throw new NotImplementedException();
        }

        public async Task<List<ProductForListingDto>> GetAll()
        {
            List<Product> products = await _productRepository.GetListAsync();

            List<ProductForListingDto> response = _mapper.Map<List<ProductForListingDto>>(products);

            return response;
        }

        public Product GetById(int id)
        {
            return _productRepository.Get(p => p.Id == id);

        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var existingProdcut = GetById(product.Id);

            existingProdcut.Name = product.Name;
            existingProdcut.UnitPrice = product.UnitPrice;

        }


    }
}
