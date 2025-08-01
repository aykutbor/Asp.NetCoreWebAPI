using DataAccess.Abstracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Entity_Framework
{
    public class EfProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            using(BaseDbContext context = new())
            {
                context.Products.Add(product);
                context.SaveChanges();
            } // Dispose etmek,  using içerisinde new'lenen nesnelerin Blok dışında garbage collector tarafından temizlenir.
        }

        public void Delete(Product product)
        {
            using (BaseDbContext context = new())
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public List<Product> GetAll()
        {
            using (BaseDbContext context = new())
            {
                return context.Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            using (BaseDbContext context = new())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        public void Update(Product product)
        {
            using (BaseDbContext context = new())
            {
                context.Products.Update(product);
                context.SaveChanges();
            }
        }
    }
}
