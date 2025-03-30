
using CompanyApp.Domain.Entities;
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Interfaces;
using CompanyApp.Infrastructure.Repositories;
using CompanyApp.Test.Fake;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CompanyApp.Test;

public class CompanyEFRepositoryTest
{
    private  Mock<IApplicationContext> _appContext;
    private  Mock<DbSet<Company>> _mockSet;
    private  CompanyEFRepository _efRepository;

    public CompanyEFRepositoryTest()
    {
        _mockSet = new Mock<DbSet<Company>>();
        _appContext = new Mock<IApplicationContext>();

        _appContext.Setup(x => x.Company).Returns(_mockSet.Object);
        _appContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
            
        _efRepository = new CompanyEFRepository(_appContext.Object);
    }

    [Fact]
    public async Task CompanyEFRepository_Create_WhenSucceeds()
    {
        //Arrange
        var company = new Company {
            Name = "Heineken NV",
            Exchange = "Euronext Amsterdam",
            Ticker = "HEIA",
            Isin = "NA0000009168",
            Website = null
        };

        //Act
        var result = await _efRepository.Create(company);

        //Assert
        Assert.IsType<int>(result);
        Assert.Equal(1, result);
        _mockSet.Verify(x => x.Add(company), Times.Once());
        _appContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task CompanyEFRepository_Delete_WhenSucceeds()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        _mockSet.Setup(x => x.FindAsync(It.IsAny<int>()))
            .ReturnsAsync(company);

        //Act
        var result = await _efRepository.Delete(company.Id);

        //Assert
        Assert.True(result);
        _mockSet.Verify(x => x.FindAsync(company.Id), Times.Once());
        _mockSet.Verify(x => x.Remove(company), Times.Once());
        _appContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task CompanyEFRepository_Update_WhenSucceeds()
    {
        //Arrange
        var company = FakeCompany.GetCompany();

        //Act
        var result = await _efRepository.Update(company);

        //Assert
        Assert.True(result);
        _mockSet.Verify(x => x.Update(company), Times.Once());
        _appContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}