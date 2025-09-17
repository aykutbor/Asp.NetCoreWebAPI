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
    Singleton => �retilen ba��ml�l�k uygulama a��k oldu�u s�rece tek bir kere newlenir. Her enjeksiyonda o instance kullan�l�r.
   
    Scoped => (API iste�i) �stek ba��na 1 instance olu�turur.

    Transient => Her ad�mda (her talepte) yeni 1 instance. Her constructor olu�turdu�umuzda yeniden olu�turur.
 */


builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();



// Assembly -> Uygulaman�n veya k�t�phanenin derlenmi� kodunu ve bu kodun �al��t�r�lmas� i�in gereken bile�enleri i�eren yap�d�r. Genellikle DLL'lerdir. AutoMapper e�leme yapaca�� i�in assembly'e gerek duyar. GetExecutingAssembly, o an sistemin �al��t��� kodun �al��t��� assembly'i verir.

// Soyutlama kulland���m�z her alanda, o soyutlaman�n kar��l��� olarak sistemde somut olarak hangisi kullan�lacak bunu belirtmemiz gerekir. E�er tan�mlanmazsa ba��ml�l�k tan�mlanmad��� i�in hata al�n�r.


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Jwt Konfig�rasyonlar�
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
