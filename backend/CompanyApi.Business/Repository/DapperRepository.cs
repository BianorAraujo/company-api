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
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }
}
