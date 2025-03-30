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
    private readonly IDbConnection _dbConnection;
    private readonly ICompanyDapperRepository _dapper;

    public DapperRepositoryTest()
    {
        _dbConnection = new SqliteConnection("Data Source=:memory:");
        _dbConnection.Open();
        _dbConnection.Execute(CreateCompanyTable);

        _dapper = new CompanyDapperRepository(_dbConnection);
    }

    [Fact]
    public async Task DapperRepository_GetAll_ReturnAllCompanies()
    {
        //Arrange
        var companies = FakeCompany.GetList();
        
        foreach (var company in companies)
        {
            await _dbConnection.ExecuteAsync(InsertCompanyTable, company);
        }

        //Act
        var result = await _dapper.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<List<Company>>(result.ToList());
        Assert.Equal(3, result.Count());
        Assert.InRange(result.Count(), 1, 3);
    }

    [Fact]
    public async Task DapperRepository_GetById_ReturnCompany_WhenSucceeds()
    {
        //Arrange
        var companies = FakeCompany.GetList();
        
        foreach (var comp in companies)
        {
            await _dbConnection.ExecuteAsync(InsertCompanyTable, comp);
        }

        var company = FakeCompany.GetCompanyById(2);

        //Act
        var result = await _dapper.GetById(2);

        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company?.Id, result?.Id);
    }

    [Fact]
    public async Task DapperRepository_GetById_ReturnNull_WhenCompanyDoesNotExist()
    {
        //Arrange
        int inexistetId = 100;

        //Act
        var result = await _dapper.GetById(inexistetId);

        //Assert
        Assert.Null(result);
        Assert.IsNotType<Company>(result);
    }

    [Fact]
    public async Task DapperRepository_GetByIsin_ReturnCompany_WhenSucceeds()
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
        await _dbConnection.ExecuteScalarAsync<int>(InsertCompanyTable, company);
        var result = await _dapper.GetByIsin(company.Isin);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company.Isin, result.Isin);
        Assert.Matches("^[a-zA-Z]{2}", result.Isin);
    }

    [Fact]
    public async Task DapperRepository_GetByIsin_ReturnNull_WhenCompanyDoesNotExist()
    {
        //Arrange
        string inexistetIsin = "ZZ1212121212";

        //Act
        var result = await _dapper.GetByIsin(inexistetIsin);

        //Assert
        Assert.Null(result);
        Assert.IsNotType<Company>(result);
    }

    [Fact]
    public async Task CompanyRepository_GetAll_ThrowsException_WhenDapperFails()
    {
        //Arrange
        _dbConnection.Close();
          
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _dapper.GetAll());
        Console.WriteLine(exception);

        //Assert
        Assert.Contains("error", exception.Message.ToLower());
    }

    [Fact]
    public async Task CompanyRepository_GetById_ThrowsException_WhenDapperFails()
    {
        //Arrange
        _dbConnection.Close();
          
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _dapper.GetById(2));
        Console.WriteLine(exception);

        //Assert
        Assert.Contains("error", exception.Message.ToLower());
    }

    [Fact]
    public async Task CompanyRepository_GetByIsin_ThrowsException_WhenDapperFails()
    {
        //Arrange
        _dbConnection.Close();
          
        //Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _dapper.GetByIsin("AB1212121212"));
        Console.WriteLine(exception);

        //Assert
        Assert.Contains("error", exception.Message.ToLower());
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