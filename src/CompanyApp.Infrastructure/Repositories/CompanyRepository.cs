using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;

namespace CompanyApp.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ICompanyDapperRepository _dapperRepo;
    private readonly ICompanyEFRepository _efRepo;

    public CompanyRepository(ICompanyDapperRepository dapperRepo, ICompanyEFRepository efRepo) 
    {
        _dapperRepo = dapperRepo;
        _efRepo = efRepo;
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
        return await _dapperRepo.GetAll();
    }

    public async Task<Company> GetById(int id)
    {
        return await _dapperRepo.GetById(id);
    }

    public async Task<Company> GetByIsin(string isin)
    {
        return await _dapperRepo.GetByIsin(isin);
    }

    public async Task<int> Create(Company company)
    {
        try
        {
            var parameters = new {
                company.Name,
                company.Exchange,
                company.Ticker,
                company.Isin,
                company.Website
            };

            company.Id = 0;

            var id = await _efRepo.Create(company);

            return id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    } 

    public async Task<bool> Update(Company company)
    {
        try
        {
            var parameters = new {
                company.Id,
                company.Name,
                company.Exchange,
                company.Ticker,
                company.Isin,
                company.Website
            };
            
            var result = await _efRepo.Update(company);

            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var result = await _efRepo.Delete(id);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}