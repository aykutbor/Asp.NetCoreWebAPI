using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.Entity_Framework
{
    // Veritabanını temsil eden dosya
    public class BaseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } // Tablonun Class karşılığı "Product", Tablo karşılığı "Products"
        public DbSet<Category> Categories { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer(
        //            @"Server=(localdb)\MSSQLLocalDB; Database=DbWebApi; Trusted_Connection=True;",
        //            options => options.EnableRetryOnFailure());
        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Database=DbWebApi2; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);

        }
    }
}

// override OnConfiguring => Database konfigüre edilirken çalışan metot.
// override OnModelCreating => İlgili databse'in altındaki modeller create edilirken çalışan metot.

// optionsBuilder, kullanarak veritabanı options'ları set edilebilir.
