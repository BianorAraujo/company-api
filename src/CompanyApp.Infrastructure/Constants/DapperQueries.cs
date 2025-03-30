
namespace CompanyApp.Infrastructure;

public abstract class DapperQueries
{
    public const string SelectAll = @"SELECT
                                        Id,
                                        Name,
                                        Exchange,
                                        Ticker,
                                        Isin,
                                        Website
                                    FROM Company";

    public const string SelectById = @"
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

    public const string SelectByIsin = @"
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

    public const string Insert = @"
                                INSERT INTO 
                                    Company (
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

    public const string Update = @"
                                UPDATE Company 
                                SET Name = @Name, 
                                    Exchange = @Exchange, 
                                    Ticker = @Ticker, 
                                    Isin = @Isin, 
                                    Website = @Website 
                                WHERE Id = @Id";
    public const string Delete = @"
                                DELETE 
                                    Company 
                                WHERE Id = @Id";
}