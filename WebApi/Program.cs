using System.Text;
using Infrastructure.AutomapperProfiles;
using Infrastructure.Database;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Регистрация DbContext до других сервисов
builder.Services.AddDbContext<Data>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<GadgetsService>();
builder.Services.AddScoped<AccountService>();

// Регистрация контроллеров
builder.Services.AddControllers();
builder.Services.AddAuthConfigureService(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

// Добавление Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User and Gadget", Version = "v1" });

    // // Add security definition for JWT Bearer tokens
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Add global security requirement
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             }
    //         },
    //         new string[] { }
    //     }
    // });
});

builder.Services.AddAutoMapper(typeof(ServiceProfile));

var app = builder.Build();

// Конфигурация HTTP конвейера
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов контроллеров
app.MapControllers();

app.Run();