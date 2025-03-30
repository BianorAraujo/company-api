using CompanyApp.Infrastructure.Repositories;
using CompanyApp.Domain.Interfaces;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "OpenCORSPolicy",
        x =>
        {
            x.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyDapperRepository, CompanyDapperRepository>();
builder.Services.AddScoped<ICompanyEFRepository, CompanyEFRepository>();
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();

var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference(option => {
    option
        .WithTitle("CompanyApp API")
        .WithTheme(ScalarTheme.Default)
        .WithDownloadButton(true)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});


app.UseHttpsRedirection();

app.UseCors("OpenCORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
