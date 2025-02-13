using System.Data;
using CompanyApi.Business.Interfaces;
using Dapper;

namespace CompanyApi.Business.Repository;

public class DapperRepository : IDapperRepository
{
    private readonly IDbConnection _connection;
    public DapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public Task<T?> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }
}
