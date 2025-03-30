using CompanyApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Infrastructure.Interfaces;

public interface IApplicationContext
{
    DbSet<Company> Company { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}