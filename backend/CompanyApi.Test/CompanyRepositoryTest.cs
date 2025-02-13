using System.Data;
using CompanyApi.Business.Interfaces;
using CompanyApi.Business.Models;
using CompanyApi.Business.Repository;
using CompanyApi.Test.Fake;
using Moq;

namespace CompanyApi.Test;

public class CompanyRepositoryTest
{
    private readonly Mock<IDapperRepository> _dapperMock;
    private readonly ICompanyRepository _companyRepository;

    public CompanyRepositoryTest()
    {
        _dapperMock = new Mock<IDapperRepository>();
        _companyRepository = new CompanyRepository(_dapperMock.Object);
    }

    [Fact]
    public async Task CompanyRepository_GetAll_ReturnAllCompanies()
    {
        //Arrange
        var companiesList = FakeCompany.GetList();

        _dapperMock.Setup(x => x.QueryAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(companiesList);

        //Act
        var result = await _companyRepository.GetAll();

        Assert.NotNull(result);
        Assert.IsType<List<Company>>(result.ToList());
        Assert.Equal(3, result.Count());
        Assert.InRange(result.Count(), 1, 3);
    }

    [Fact]
    public async Task CompanyRepository_GetAll_ReturnEmptyList_WhenNoCompaniesExist()
    {
        // Arrange
        List<Company> companiesList = FakeCompany.GetEmptyList();

        _dapperMock.Setup(x => x.QueryAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(companiesList);

        // Act
        var result = await _companyRepository.GetAll();

        // Assert
        Assert.Empty(result);
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Fact]
    public async Task CompanyRepository_GetById_ReturnCompanyById()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(company);

        //Act
        var result = await _companyRepository.GetById(company.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Same(company, result);
        Assert.Matches("^[a-zA-Z]{2}", result.Isin);
    }

    [Fact]
    public async Task CompanyRepository_GetByIsin_ReturnCompanyByIsin()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(company);

        //Act
        var result = await _companyRepository.GetByIsin(company.Isin);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company.Isin, result.Isin);
        Assert.Matches("^[a-zA-Z]{2}", result.Isin);
    }

    [Fact]
    public async Task CompanyRepository_Create_ReturnCompanyCreated()
    {
        //Arrange
        var company = new Company {
            Name = "Heineken NV",
            Exchange = "Euronext Amsterdam",
            Ticker = "HEIA",
            Isin = "NL0000009165",
            Website = null
        };

        _dapperMock.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(4);

        //Act
        var result = await _companyRepository.Create(company);
        
        //Assert
        Assert.IsType<int>(result);
        Assert.Equal(4, result);
        Assert.IsNotType<Company>(result);
    }

    [Fact]
    public async Task CompanyRepository_Update_ReturnCompanyUpdated()
    {
        //Arrange
        var company = FakeCompany.GetCompany();
        company.Name = "New Company name";

        _dapperMock.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
            .ReturnsAsync(1);

        //Act
        var result = await _companyRepository.Update(company);

        //Assert
        Assert.True(result);

    }
}
