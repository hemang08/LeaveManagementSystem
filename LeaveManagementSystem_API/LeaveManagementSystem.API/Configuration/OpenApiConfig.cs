using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

namespace LeaveManagementSystem.API.Configuration
{
    public static class OpenApiConfig
    {
        public static void AddOpenApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"Leave Management System API",
                    Description = "Leave Management System .NET 9 Core Web API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    option.IncludeXmlComments(xmlPath);
                }

                option.EnableAnnotations();

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void UseOpenApiConfiguration(this WebApplication app)
        {

            //if (app.Environment.IsDevelopment())
            //{
                app.MapOpenApi();

                // Swagger UI
                app.UseSwagger(opt =>
                {
                    opt.RouteTemplate = "openapi/{documentName}.json";
                });

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/openapi/v1.json", "Leave Management System Web Api");
                    options.RoutePrefix = "swagger";
                });

                // Scalar UI
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("Leave Management System API - Scalar")
                        .WithTheme(ScalarTheme.BluePlanet)
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });
            //}
            // ReDoc UI
            app.UseReDoc(options =>
            {
                options.RoutePrefix = "redoc";
                options.DocumentTitle = "Leave Management System API - ReDoc";
                options.SpecUrl($"/openapi/v1.json");
            });
        }
    }
}