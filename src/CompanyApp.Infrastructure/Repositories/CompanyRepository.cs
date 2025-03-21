using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;
using CompanyApp.Infrastructure.Data;

namespace CompanyApp.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IDapperRepository _dapper;
    private readonly ICompanyDapperRepository _dapperRepo;
    private readonly ApplicationContext _appContext;

    public CompanyRepository(IDapperRepository dapper, ICompanyDapperRepository dapperRepo, ApplicationContext appContext) 
    {
        _dapper = dapper;
        _dapperRepo = dapperRepo;
        _appContext = appContext;
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

            _appContext.Company.Add(company);// _dapper.ExecuteScalarAsync<int>(DapperQueries.Insert, parameters);
             var id = await _appContext.SaveChangesAsync();

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
            
            var result = await _dapper.ExecuteAsync(DapperQueries.Update, parameters);

            if(result != 1)
                return false;
            
            return true;

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
            var parameters = new {
                id
            };
            
            var result = await _dapper.ExecuteAsync(DapperQueries.Delete, parameters);

            if(result != 1)
                return false;
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}