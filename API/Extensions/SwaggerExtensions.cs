using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "Merchines API", Version = "V1" });
               var securitySchema = new OpenApiSecurityScheme
               {
                   Description = "JWT Auth BearerScheme",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.Http,
                   Scheme = "bearer",
                   Reference = new OpenApiReference
                   {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                   }
               };
               c.AddSecurityDefinition("Bearer", securitySchema);
               
               var securityRequirements = new OpenApiSecurityRequirement
               {
                   {
                       securitySchema,
                       new[] {"Bearer"}
                   }
               };
               c.AddSecurityRequirement(securityRequirements);
           });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Merchines Api V1"); });

            return app;
        }
    }
}