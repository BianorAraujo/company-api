using System.Data;
using CompanyApi.Business.Repository;
using CompanyApi.Business.Interfaces;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDbConnection>(x =>
    new SqlConnection(builder.Configuration.GetConnectionString("DbConnection"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDapperRepository, DapperRepository>();

var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference(option => {
    option
        .WithTitle("Auth API")
        .WithTheme(ScalarTheme.Default)
        .WithDownloadButton(true)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
