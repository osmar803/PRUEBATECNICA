using Microsoft.EntityFrameworkCore;
using Domain.repository;
using  Application.Mappings;
using Prueba.Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Prueba.Infrastructure.Data;
using AutoMapper;






var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Configuración de DbContext (MySQL o SQL Server)
// ----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ----------------------
// AutoMapper
// ----------------------
builder.Services.AddAutoMapper(
    typeof(DomainToDtoProfile),
    typeof(DtoToDomainProfile)
);


// ----------------------
// Repositorios y servicios
// ----------------------




// ----------------------
// CORS (para WinForms/Front-end o Angular si usas)
// ----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ----------------------
// JWT Authentication (opcional, si tu proyecto lo requiere)
// ----------------------

// ----------------------
// Swagger/OpenAPI
// ----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Prueba API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido de su token JWT"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ----------------------
// Controllers
// ----------------------
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// ----------------------
// Middleware pipeline
// ----------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API v1");
});

app.UseHttpsRedirection();

app.UseCors("LocalPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
