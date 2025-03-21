using CompanyApp.Domain.Entities;

namespace CompanyApp.Domain.Interfaces;

public interface ICompanyDapperRepository
{
    Task<IEnumerable<Company>> GetAll();
    Task<Company> GetById(int id);
    Task<Company> GetByIsin(string isin);
}