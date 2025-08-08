using Business.Abstracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        List<Category> _categories;

        public CategoryManager()
        {
            _categories = new List<Category>();
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(int id)
        {
            var result = _categories.Find(x => x.Id == id);
            if(result == null)
            {
                throw new NullReferenceException("Id is not valid!");
            }
            _categories.Remove(result);
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category GetById(int id)
        {
            var result = _categories.Find(x => x.Id == id);
            if (result == null)
            {
                throw new NullReferenceException("Id is not valid!");
            }
            return result;
        }

        public void Update(Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);

            if(existingCategory == null)
            {
                throw new ArgumentException("Güncellemek istenen kategori bulunamadı");
            }

            existingCategory.Name = category.Name;
        }
    }
}
