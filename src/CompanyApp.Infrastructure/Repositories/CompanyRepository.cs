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
            const string sql = @"SELECT
                                    Id,
                                    Name,
                                    Exchange,
                                    Ticker,
                                    Isin,
                                    Website
                                FROM Company";

            var companies = await _dapper.QueryAsync<Company>(sql);

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
            const string sql = @"
                SELECT
                    Id,
                    Name,
                    Exchange,
                    Ticker,
                    Isin,
                    Website
                FROM Company
                WHERE
                    Id = @Id";

            var company = await _dapper.QueryFirstOrDefaultAsync<Company>(sql, new { Id = id });
            
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
            const string sql = @"
                SELECT
                    Id,
                    Name,
                    Exchange,
                    Ticker,
                    Isin,
                    Website
                FROM Company
                WHERE
                    Isin = @Isin";

            var company = await _dapper.QueryFirstOrDefaultAsync<Company>(sql, new { Isin = isin});
            
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

            const string sql = @"
                INSERT INTO Company (
                                Name,
                                Exchange,
                                Ticker,
                                Isin,
                                Website
                            )
                            VALUES (
                                @Name,
                                @Exchange,
                                @Ticker,
                                @Isin,
                                @Website
                            );

                SELECT SCOPE_IDENTITY();";

            var id = await _dapper.ExecuteScalarAsync<int>(sql, parameters);

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

            const string sql = @"
                UPDATE Company 
                SET Name = @Name, 
                    Exchange = @Exchange, 
                    Ticker = @Ticker, 
                    Isin = @Isin, 
                    Website = @Website 
                WHERE Id = @Id";
            
            var result = await _dapper.ExecuteAsync(sql, parameters);

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