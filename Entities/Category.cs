using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category : Entity
    {
        public Category()
        {
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } 
    }
}

// virtual, veritabanında gerçekten oluşmayan sanal bir alan için kullanılmıştır.

// NOT: Constructor alanlarda virtual alanlara yer verilmemelidir! Çünkü onlar ilişki sonucu otomatik oluşan alanlardır.