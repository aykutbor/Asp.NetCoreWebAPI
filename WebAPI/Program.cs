using Business.Abstracts;
using Business.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes.Entity_Framework;
using DataAccess.Concretes.InMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
    Singleton => Üretilen baðýmlýlýk uygulama açýk olduðu sürece tek bir kere newlenir. Her enjeksiyonda o instance kullanýlýr.
   
    Scoped => (API isteði) Ýstek baþýna 1 instance oluþturur.

    Transient => Her adýmda (her talepte) yeni 1 instance. Her constructor oluþturduðumuzda yeniden oluþturur.
 */
builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<ICategoryService, CategoryManager>();
builder.Services.AddSingleton<IProductRepository, EfProductRepository>();
builder.Services.AddDbContext<BaseDbContext>();

// Soyutlama kullandýðýmýz her alanda, o soyutlamanýn karþýlýðý olarak sistemde somut olarak hangisi kullanýlacak bunu belirtmemiz gerekir. Eðer tanýmlanmazsa baðýmlýlýk tanýmlanmadýðý için hata alýnýr.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
