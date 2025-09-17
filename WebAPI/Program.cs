using Business.Abstracts;
using Business.Concretes;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Extentions;
using DataAccess.Abstracts;
using DataAccess.Concretes.Entity_Framework;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Business;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;


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


builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();



// Assembly -> Uygulamanýn veya kütüphanenin derlenmiþ kodunu ve bu kodun çalýþtýrýlmasý için gereken bileþenleri içeren yapýdýr. Genellikle DLL'lerdir. AutoMapper eþleme yapacaðý için assembly'e gerek duyar. GetExecutingAssembly, o an sistemin çalýþtýðý kodun çalýþtýðý assembly'i verir.

// Soyutlama kullandýðýmýz her alanda, o soyutlamanýn karþýlýðý olarak sistemde somut olarak hangisi kullanýlacak bunu belirtmemiz gerekir. Eðer tanýmlanmazsa baðýmlýlýk tanýmlanmadýðý için hata alýnýr.


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Jwt Konfigürasyonlarý
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
