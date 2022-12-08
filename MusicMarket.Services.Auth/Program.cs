using Microsoft.EntityFrameworkCore;
using MusicMarket.Services.Auth.DbStuff;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MusicMarket.Services.Auth;
using MusicMarket.Services.Auth.DbStuff.Repositories.IRepositories;
using MusicMarket.Services.Auth.DbStuff.Repositories;
using MusicMarket.Services.Auth.Extentions;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ?????????, ????? ?? ?????????????? ???????? ??? ????????? ??????
            ValidateIssuer = true,
            // ??????, ?????????????? ????????
            ValidIssuer = AuthOptions.ISSUER,
            // ????? ?? ?????????????? ??????????? ??????
            ValidateAudience = true,
            // ????????? ??????????? ??????
            ValidAudience = AuthOptions.AUDIENCE,
            // ????? ?? ?????????????? ????? ?????????????
            ValidateLifetime = false,
            // ????????? ????? ????????????
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ????????? ????? ????????????
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }

    });
});

var connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicMarket.Services.Auth;Integrated Security=True;";

builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(connectString));


builder.Services.RegisterServices(builder.Configuration);//Custom DI Container
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetService<IDbSeed>();
    dbInitializer.Initialize();

}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
