using CompanyApp.Domain.Entities;
using CompanyApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Infrastructure.Data;

public class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Company> Company { get; set; }
}