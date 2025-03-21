using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyApp.Infrastructure.Data;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDbConnection>(x => new SqlConnection(connectionString));

        return services;
    }
}