using IngressosAPI.Filters;
using IngressosAPI.Interfaces;
using IngressosAPI.Repositories;
using IngressosAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Registrando filtros
builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddScoped<ActionFilter>();
builder.Services.AddScoped<ExceptionFilter>();

// Configurando controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurando o JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configurando CosmosDb
builder.Services.AddSingleton<CosmosClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    string account = configuration["CosmosDb:AccountEndpoint"];
    string key = configuration["CosmosDb:AccountKey"];
    return new CosmosClient(account, key);
});

// Mapeando Interfaces e Repositórios do CosmosDB
builder.Services.AddScoped<IIngressoRepository, IngressoRepository>();
builder.Services.AddScoped<IIngressoService, IngressoService>();

// Mapeando Interfaces e Services do MessageBus
builder.Services.AddScoped<IServiceBusService, AzureServiceBusService>();
builder.Services.AddScoped<IMessageServiceBusConsumerService, MessageServiceBusConsumerService>();

// Adicionando suporte ao GRPC
builder.Services.AddGrpc();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Mapeando o serviço GRPC
app.MapGrpcService<GrpcIngressoService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
