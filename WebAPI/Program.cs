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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TokenOptions = Core.Utilities.JWT.TokenOptions;
using Core.Utilities.Encryption;


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

//******** string securityKey = builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecurityKey"); ********** // -> appsettings.json'dan veya appsetting.development i�erisinden veri okumay� sa�lar. "Get" metodu TokenOptions'u (section) komple bir b�t�n olarak al�r i�erisindekiler dahil, GetValue ise sadece ilgili k�sm� al�r. 

// Yukar�daki �rnekteki gibi tek tek okumak yerine appsettings.developmentteki veriler bir model olarak olu�turuldu. Bu class sayesinde tek tek okumak yerine do�rudan class �zerinden okunur.

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Jwt Konfig�rasyonlar�
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, // secret key'e g�re imzalan�p imzalanmad��� kontrol edilir.
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

        };
    });

// Token'in validasyon parametreleri belirlenir. .Net, JWT'yi otomatik olarak tan�ml� kodlarla valide etmeye �al���yor. Valide ederken kullanaca�� de�erler burada set edilir.

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
