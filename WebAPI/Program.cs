using Business.Abstracts;
using Business.Concretes;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Extentions;
using DataAccess.Abstracts;
using DataAccess.Concretes.Entity_Framework;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
    Singleton => �retilen ba��ml�l�k uygulama a��k oldu�u s�rece tek bir kere newlenir. Her enjeksiyonda o instance kullan�l�r.
   
    Scoped => (API iste�i) �stek ba��na 1 instance olu�turur.

    Transient => Her ad�mda (her talepte) yeni 1 instance. Her constructor olu�turdu�umuzda yeniden olu�turur.
 */
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddDbContext<BaseDbContext>();

// Soyutlama kulland���m�z her alanda, o soyutlaman�n kar��l��� olarak sistemde somut olarak hangisi kullan�lacak bunu belirtmemiz gerekir. E�er tan�mlanmazsa ba��ml�l�k tan�mlanmad��� i�in hata al�n�r.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
