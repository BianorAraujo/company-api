using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;

namespace CompanyApp.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IDapperRepository _dapper;

    public CompanyRepository(IDapperRepository dapper) 
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
        try
        {
            var companies = await _dapper.QueryAsync<Company>(DapperQueries.SelectAll);

            return companies;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Company> GetById(int id)
    {
        try
        {
            var company = await _dapper.QueryFirstOrDefaultAsync<Company>(DapperQueries.SelectById, new { Id = id });
            
            return company;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Company> GetByIsin(string isin)
    {
        try
        {
            var company = await _dapper.QueryFirstOrDefaultAsync<Company>(DapperQueries.SelectByIsin, new { Isin = isin});
            
            return company;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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

            var id = await _dapper.ExecuteScalarAsync<int>(DapperQueries.Insert, parameters);

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