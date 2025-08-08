using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } 
    }
}

// virtual, veritabanında gerçekten oluşmayan sanal bir alan için kullanılmıştır.

// NOT: Constructor alanlarda virtual alanlara yer verilmemelidir! Çünkü onlar ilişki sonucu otomatik oluşan alanlardır.