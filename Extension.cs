using Bowllytics.Data;
using Bowllytics.Endpoints;
using Bowllytics.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Bowllytics;

public static class Extension
{
    public static IServiceCollection RegisterProgram(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BowlsDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<BowlsDbContext>();

        services.AddOpenApi();
        services.AddCors(c =>
        {
            c.AddPolicy("AllowFrontend", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyMethod();
            });
        });

        services.AddAuthentication();
        services.AddAuthorization();
        services.AddEndpoints();
        
        return services;
    }
    
    public static WebApplication UseProgram(this WebApplication app)
    {
        app.MapGroup("/api/auth").MapIdentityApi<User>();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        app.UseCors("AllowFrontend");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapEndpoints();
        
        return app;
    }
}