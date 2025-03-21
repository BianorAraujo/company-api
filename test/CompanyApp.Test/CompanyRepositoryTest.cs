using System.Data;
using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;
using CompanyApp.Infrastructure.Repositories;
using CompanyApp.Test.Fake;
using Moq;

namespace CompanyApp.Test;

public class CompanyRepositoryTest
{
    private readonly Mock<IDapperRepository> _dapperMock;
    private readonly Mock<ICompanyDapperRepository> _dapperRepoMock;
    private readonly ICompanyRepository _companyRepository;

    public CompanyRepositoryTest()
    {
        _dapperMock = new Mock<IDapperRepository>();
        _dapperRepoMock = new Mock<ICompanyDapperRepository>();
        _companyRepository = new CompanyRepository(_dapperMock.Object, _dapperRepoMock.Object);
    }

    [Fact]
    public async Task CompanyRepository_GetAll_ReturnAllCompanies()
    {
        //Arrange
        var companiesList = FakeCompany.GetList();


        // _dapperMock.Setup(x => x.QueryAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
        //     .ReturnsAsync(companiesList);
        _dapperRepoMock.Setup(x => x.GetAll()).ReturnsAsync(companiesList);

        // //Act
        var result = await _companyRepository.GetAll();

        // //Assert
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

        // _dapperMock.Setup(x => x.QueryAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
        //     .ReturnsAsync(companiesList);
        _dapperRepoMock.Setup(x => x.GetAll()).ReturnsAsync(companiesList);

        // Act
        var result = await _companyRepository.GetAll();

        // Assert
        Assert.Empty(result);
        Assert.NotNull(result);
        Assert.False(result.Any());
    }

    [Fact]
    public async Task CompanyRepository_GetAll_ThrowsException_WhenDapperFails()
    {
        //Arrage
        // _dapperMock.Setup(x => x.QueryAsync<Company>(
        //     It.IsAny<string>(),
        //     It.IsAny<object>(),
        //     It.IsAny<IDbTransaction>(),
        //     It.IsAny<int?>(),
        //     It.IsAny<CommandType?>()
        // )).ThrowsAsync(new Exception("Database error"));
        _dapperRepoMock.Setup(x => x.GetAll()).ThrowsAsync(new Exception("Database error"));

        //Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyRepository.GetAll());
    }

    [Fact]
    public async Task CompanyRepository_GetById_ReturnCompanyById()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
        //     .ReturnsAsync(company);
        _dapperRepoMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(company);

        //Act
        var result = await _companyRepository.GetById(company.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Same(company, result);
        Assert.Matches("^[a-zA-Z]{2}", result.Isin);
    }

    [Fact]
    public async Task CompanyRepository_GetById_ReturnNullCompany()
    {
        //Arrange
        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), null, null, null, null))
        //     .ReturnsAsync((Company)null);
        _dapperRepoMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Company)null);

        //Act
        var result = await _companyRepository.GetById(999);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CompanyRepository_GetById_ThrowsException_WhenDapperFails()
    {
        //Arrage
        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(
        //     It.IsAny<string>(),
        //     It.IsAny<object>(),
        //     It.IsAny<IDbTransaction>(),
        //     It.IsAny<int?>(),
        //     It.IsAny<CommandType?>()
        // )).ThrowsAsync(new Exception("Database error"));
        _dapperRepoMock.Setup(x => x.GetById(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

        //Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyRepository.GetById(1));
    }

    [Fact]
    public async Task CompanyRepository_GetByIsin_ReturnCompanyByIsin()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
        //     .ReturnsAsync(company);
        _dapperRepoMock.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync(company);

        //Act
        var result = await _companyRepository.GetByIsin(company.Isin);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Company>(result);
        Assert.Equal(company.Isin, result.Isin);
        Assert.Matches("^[a-zA-Z]{2}", result.Isin);
    }

    [Fact]
    public async Task CompanyRepository_GetByIsin_ReturnNullCompany()
    {
        //Arrange
        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(It.IsAny<string>(), null, null, null, null))
        //     .ReturnsAsync((Company)null);
        _dapperRepoMock.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync((Company)null);

        //Act
        var result = await _companyRepository.GetByIsin("AA1212121212");

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CompanyRepository_GetByIsin_ThrowsException_WhenDapperFails()
    {
        //Arrage
        // _dapperMock.Setup(x => x.QueryFirstOrDefaultAsync<Company>(
        //     It.IsAny<string>(),
        //     It.IsAny<object>(),
        //     It.IsAny<IDbTransaction>(),
        //     It.IsAny<int?>(),
        //     It.IsAny<CommandType?>()
        // )).ThrowsAsync(new Exception("Database error"));
        _dapperRepoMock.Setup(x => x.GetByIsin(It.IsAny<string>())).ThrowsAsync(new Exception("Database error"));

        //Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyRepository.GetByIsin("INVALID_ISIN"));
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
    public async Task CompanyRepository_Create_ThrowsException_WhenDapperFails()
    {
        //Arrage
        var company = new Company {
            Name = "Heineken NV",
            Exchange = "Euronext Amsterdam",
            Ticker = "HEIA",
            Isin = "NL0000009165",
            Website = null
        };

        _dapperMock.Setup(x => x.ExecuteScalarAsync<int>(
            It.IsAny<string>(),
            It.IsAny<object>(),
            It.IsAny<IDbTransaction>(),
            It.IsAny<int?>(),
            It.IsAny<CommandType?>()
        )).ThrowsAsync(new Exception("Database error"));

        //Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyRepository.Create(company));
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

    [Fact]
    public async Task CompanyRepository_Update_ReturnEmptyCompany()
    {
        //Arrange
        var company = FakeCompany.GetCompany();
        company.Name = "New Company name";

        _dapperMock.Setup(x => x.ExecuteAsync(It.IsAny<string>(), null, null, null, null))
            .ReturnsAsync(0);

        //Act
        var result = await _companyRepository.Update(company);

        //Assert
        Assert.False(result);


    }

    [Fact]
    public async Task CompanyRepository_Update_ThrowsException_WhenDapperFails()
    {
        //Arrage
        var company = FakeCompany.GetCompany();
        company.Name = "New Company name";

        _dapperMock.Setup(x => x.ExecuteAsync(
            It.IsAny<string>(),
            It.IsAny<object>(),
            It.IsAny<IDbTransaction>(),
            It.IsAny<int?>(),
            It.IsAny<CommandType?>()
        )).ThrowsAsync(new Exception("Database error"));

        //Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyRepository.Update(company));
    }
}
