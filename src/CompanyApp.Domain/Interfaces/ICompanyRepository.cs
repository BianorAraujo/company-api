using CompanyApp.Domain.Entities;

namespace CompanyApp.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAll();
    Task<Company> GetById(int id);
    Task<Company> GetByIsin(string isin);
    Task<int> Create(Company company);
    Task<bool> Update(Company company);
}