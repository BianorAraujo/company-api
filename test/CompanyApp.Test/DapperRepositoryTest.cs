using System.Data;
using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;
using CompanyApp.Infrastructure.Repositories;
using CompanyApp.Test.Fake;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CompanyApp.Test;

public class DapperRepositoryTest
{
    public readonly IDbConnection _dbConnection;
    public readonly IDapperRepository _dapperRepository;

    public DapperRepositoryTest()
    {
        _dbConnection = new SqliteConnection("Data Source=:memory:");
        _dbConnection.Open();
        _dbConnection.Execute(CreateCompanyTable);
        _dapperRepository = new DapperRepository(_dbConnection);

        var companies = FakeCompany.GetList();
        foreach (var company in companies)
        {
            _dbConnection.Execute(InsertCompanyTable, company);
        }
    }

    [Fact]
    public async Task DapperRepository_QueryAsync_ReturnListOfCompanies()
    {
        //Act
        var result = await _dapperRepository.QueryAsync<Company>("SELECT * FROM Company");

        //Assert
        Assert.NotNull(result);
        Assert.IsType<List<Company>>(result);
        Assert.Equal(FakeCompany.GetList().Count, result.Count());
    }

    [Fact]
    public async Task DapperRepository_QueryFirstOrDefaultAsync_ReturnCompany()
    {
        //Arrange
        var company = FakeCompany.GetCompanyById(2);

        //Act
        var result = await _dapperRepository.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Company WHERE Id = @Id", new { Id = 2 });

        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company?.Id, result?.Id);
    }

    [Fact]
    public async Task DapperRepository_QueryFirstOrDefaultAsync_ReturnNull_WhenCompanyDoesNotExist()
    {
        //Arrange
        var company = FakeCompany.GetCompanyById(100);

        //Act
        var result = await _dapperRepository.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Company WHERE Id = @Id", new { Id = 100 });

        //Assert
        Assert.Null(result);
        Assert.IsNotType<Company>(result);
    }

    [Fact]
    public async Task DapperRepository_ExecuteScalarAsync_ReturnId()
    {
        //Arrange
        var company = new Company
        {
            Id = 4,
            Name = "Test Company",
            Exchange = "Test Exchange",
            Ticker = "TEST",
            Isin = "TE1234567890",
            Website = "http://test.com"
        };

        //Act
        await _dapperRepository.ExecuteScalarAsync<int>(InsertCompanyTable, company);
        var result = await _dapperRepository.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Company WHERE Isin = @Isin", new { company.Isin });

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company.Id, result.Id);
    }

    [Fact]
    public async Task DapperRepository_ExecuteAsync_ReturnId()
    {
        //Arrange
        var company = new Company
        {
            Id = 1,
            Name = "Apple Inc.",
            Exchange = "NASDAQ",
            Ticker = "AAPL",
            Isin = "US0378331005",
            Website = "http://www.teste.com"
        };

        //Act
        var id = await _dapperRepository.ExecuteAsync(UpdateCompanyTable, company);
        var result = await _dapperRepository.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Company WHERE Id = @Id", new { Id = id });

        //Assert
        Assert.IsType<int>(id);
        Assert.Equal(company.Website, result?.Website);
    }


    private const string CreateCompanyTable = @"CREATE TABLE Company (
                                            Id INTEGER PRIMARY KEY,
                                            Name TEXT NOT NULL,
                                            Exchange TEXT NOT NULL,
                                            Ticker TEXT NOT NULL,
                                            Isin TEXT NOT NULL,
                                            Website TEXT NULL
                                        );";
    
    private const string InsertCompanyTable = @"INSERT INTO 
                                        Company (Name, Exchange, Ticker, Isin, Website) 
                                        VALUES (@Name, @Exchange, @Ticker, @Isin, @Website)";

    private const string UpdateCompanyTable = @"UPDATE Company 
                                        SET Name = @Name, 
                                            Exchange = @Exchange, 
                                            Ticker = @Ticker, 
                                            Isin = @Isin, 
                                            Website = @Website 
                                        WHERE Id = @Id";       
}