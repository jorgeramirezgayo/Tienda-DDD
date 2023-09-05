using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tienda.Application.Authentication;
using Tienda.Application.Authorization;
using Tienda.Application.Queries;
using Tienda.Domain.AggregatesModel;
using Tienda.Infraestructure.Data;
using Tienda.Infraestructure.Repositories;

namespace Tienda.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TiendaDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            },
            ServiceLifetime.Scoped //Showing explicitly that the DbContext is shared across the HTTP request scope
            );

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            // Add any repository...

            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IProductoQueries, ProductoQueries>(c => new ProductoQueries(connectionString));
            services.AddScoped<IPedidoQueries, PedidoQueries>(c => new PedidoQueries(connectionString));
            services.AddScoped<IClienteQueries, ClienteQueries>(c => new ClienteQueries(connectionString));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // JWT Authorization
            services.AddScoped<IJwtUtils, JwtUtils>();

            // Authentication
            services.AddScoped<IAuthentication, Authentication>();

            return services;
        }

        public static IServiceCollection CustomizeSwaggerDefinition(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthenticatedAPI", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },

                        new string[] {}
                    }
                });

                // options.OperationFilter<AddSwaggerHeaderParameter>();
            });

            return services;
        }
    }
}
