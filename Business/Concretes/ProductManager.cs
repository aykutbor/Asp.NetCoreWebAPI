using Business.Abstracts;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.DataAccess;
using DataAccess.Abstracts;
using Entities;
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

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {

            if(product.UnitPrice < 0)
            {
                throw new BusinessException("Ürün fiyatı 0'dan küçük olamaz.");
            }

            Product? productWithSameName = await _productRepository.GetAsync(p => p.Name == product.Name);
            if (productWithSameName is not null)
                throw new System.Exception("Aynı isimde 2. ürün eklenemez.");

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

        public async Task<List<Product>> GetAll()
        {

            return await _productRepository.GetListAsync();

            //return _productRepository
            // .GetList(p => p.UnitPrice > 100)   // sadece predicate varsa
            // .OrderBy(p => p.Name)              // LINQ ile sırala
            // .ToList();
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
