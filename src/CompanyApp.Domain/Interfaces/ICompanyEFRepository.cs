using CompanyApp.Domain.Entities;

namespace CompanyApp.Domain.Interfaces;

public interface ICompanyEFRepository
{
    Task<int> Create(Company company);
    Task<bool> Update(Company company);
    Task<bool> Delete(int id);
}