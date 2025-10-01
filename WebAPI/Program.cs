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
    Singleton => Üretilen baðýmlýlýk uygulama açýk olduðu sürece tek bir kere newlenir. Her enjeksiyonda o instance kullanýlýr.
   
    Scoped => (API isteði) Ýstek baþýna 1 instance oluþturur.

    Transient => Her adýmda (her talepte) yeni 1 instance. Her constructor oluþturduðumuzda yeniden oluþturur.
 */


builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices();



// Assembly -> Uygulamanýn veya kütüphanenin derlenmiþ kodunu ve bu kodun çalýþtýrýlmasý için gereken bileþenleri içeren yapýdýr. Genellikle DLL'lerdir. AutoMapper eþleme yapacaðý için assembly'e gerek duyar. GetExecutingAssembly, o an sistemin çalýþtýðý kodun çalýþtýðý assembly'i verir.

// Soyutlama kullandýðýmýz her alanda, o soyutlamanýn karþýlýðý olarak sistemde somut olarak hangisi kullanýlacak bunu belirtmemiz gerekir. Eðer tanýmlanmazsa baðýmlýlýk tanýmlanmadýðý için hata alýnýr.

//******** string securityKey = builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecurityKey"); ********** // -> appsettings.json'dan veya appsetting.development içerisinden veri okumayý saðlar. "Get" metodu TokenOptions'u (section) komple bir bütün olarak alýr içerisindekiler dahil, GetValue ise sadece ilgili kýsmý alýr. 

// Yukarýdaki örnekteki gibi tek tek okumak yerine appsettings.developmentteki veriler bir model olarak oluþturuldu. Bu class sayesinde tek tek okumak yerine doðrudan class üzerinden okunur.

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Jwt Konfigürasyonlarý
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, // secret key'e göre imzalanýp imzalanmadýðý kontrol edilir.
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

        };
    });

// Token'in validasyon parametreleri belirlenir. .Net, JWT'yi otomatik olarak tanýmlý kodlarla valide etmeye çalýþýyor. Valide ederken kullanacaðý deðerler burada set edilir.

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
