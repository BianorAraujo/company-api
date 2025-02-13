using CompanyApi.Business.Models;

namespace CompanyApi.Test.Fake;

public static class FakeCompany
{
    public static List<Company> list = new List<Company>
        {
            new Company {
                Id = 1,
                Name = "Apple Inc.",
                Exchange = "NASDAQ",
                Ticker = "AAPL",
                Isin = "US0378331005",
                Website = "http://www.apple.com"
                },
            new Company {
                Id = 2,
                Name = "British Airways Plc",
                Exchange = "Pink Sheets",
                Ticker = "BAIRY",
                Isin = "US1104193065",
                Website = null
            },
            new Company {
                Id = 3,
                Name = "Porsche Automobil",
                Exchange = "Deutsche Börse",
                Ticker = "PAH3",
                Isin = "DE000PAH0038",
                Website = "https://www.porsche.com/"
            }
        };

    public static List<Company> GetList()
    {
        return list;
    }

    public static Company GetCompany()
    {
        return list[0];
    }

    public static List<Company> GetEmptyList()
    {
        return new List<Company> ();
    }
}