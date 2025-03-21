using CompanyApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Company> Company { get; set; }
}