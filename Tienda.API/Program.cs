using AutoMapper;
using MediatR;
using System.Reflection;
using Tienda.API.Extensions;
using Tienda.API.Middlewares;
using Tienda.Application.Handlers;
using Tienda.Application.Helpers;
using Tienda.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(typeof(CreateProductoCommandHandler).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(typeof(CreateClienteCommandHandler).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(typeof(CreatePedidoCommandHandler).GetTypeInfo().Assembly);

// AutoMapper
List<Profile> mapperProfiles = new List<Profile> { new MapperConfig() };
builder.Services.AddSingleton(new MapperConfiguration(mc => mc.AddProfiles(mapperProfiles)).CreateMapper());

// Custom
builder.Services.AddCustomDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddRepositories();
builder.Services.AddQueries(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddServices();
builder.Services.CustomizeSwaggerDefinition();


// appsettings.json
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
