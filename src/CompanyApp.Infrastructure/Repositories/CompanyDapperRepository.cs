using System.Data;
using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;
using Dapper;

namespace CompanyApp.Infrastructure.Repositories;

public class CompanyDapperRepository : ICompanyDapperRepository
{
    private readonly IDbConnection _dbConnection;
    public CompanyDapperRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
        try
        {
            var companies = await _dbConnection.QueryAsync<Company>(DapperQueries.SelectAll);

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
            var company = await _dbConnection.QueryFirstOrDefaultAsync<Company>(DapperQueries.SelectById, new { Id = id });
            
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
            var company = await _dbConnection.QueryFirstOrDefaultAsync<Company>(DapperQueries.SelectByIsin, new { Isin = isin});
            
            return company;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}