using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Entity_Framework
{
    // Veritabanını temsil eden dosya
    public class BaseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } // Tablonun Class karşılığı "Product", Tablo karşılığı "Products"
        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database=DbWebApi2; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products"); // Tablo ismi değiştirilir.
            modelBuilder.Entity<Product>().HasOne(i => i.Category); // Tablo ilişkileri kurulur.
            modelBuilder.Entity<Product>().Property(i => i.Name).HasColumnName("Name").HasMaxLength(50); // Tablodaki kolon ismi değiştirilebilir.


            // OnModelCreating -> Genel veritabanı özellikleriyle değil, direkt modelin özellikleriyle çalışan metottur. Özetle, veritabanındaki özelleştirmelerimizi yapıyoruz.


            // Seed Data -> Sizin veritabanını oluştururken kullanabileceğiniz test verilerini otomatik eklemek demektir. Bu örnek veriler veritabanında varsayılan olarak görünür.(Migration devreye alındığı zaman). CodeFirst yapılarında db'den veri oluşturmamak için test amaçlı kullanılır.

            // Aşağıdaki yapı Seed Data'dır.
            Category category = new Category(1, "Giyim");
            Category category2 = new(2, "Elektronik");

            Product product = new Product(1, "Kazak", 500, 50, 1);


            modelBuilder.Entity<Category>().HasData(category, category2);
            modelBuilder.Entity<Product>().HasData(product);

            base.OnModelCreating(modelBuilder);
        }
    }
}

// override OnConfiguring => Database konfigüre edilirken çalışan metot.
// override OnModelCreating => İlgili databse'in altındaki modeller create edilirken çalışan metot.

// optionsBuilder, kullanarak veritabanı options'ları set edilebilir.

