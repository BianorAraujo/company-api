using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;
using CompanyApp.Infrastructure.Interfaces;

namespace CompanyApp.Infrastructure.Repositories;

public class CompanyEFRepository : ICompanyEFRepository
{
    private readonly IApplicationContext _appContext;

    public CompanyEFRepository(IApplicationContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<int> Create(Company company)
    {
        _appContext.Company.Add(company);
        var id = await _appContext.SaveChangesAsync();

        return id;
    }

    public async Task<bool> Delete(int id)
    {
        var company = await _appContext.Company.FindAsync(id);

        if(company == null)
            return false;

        _appContext.Company.Remove(company);
        
        await _appContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Company company)
    {
        _appContext.Company.Update(company);

        await _appContext.SaveChangesAsync();

        return true;
    }
}