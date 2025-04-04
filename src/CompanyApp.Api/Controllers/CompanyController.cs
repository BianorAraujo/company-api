using AutoMapper;
using CompanyApp.Domain.Interfaces;
using CompanyApp.Domain.Entities;
using CompanyApp.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var companies = await _repository.GetAll();

            if (companies != null)
                return Ok(companies);
            else
                return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var company = await _repository.GetById(id);

            if (company != null)
                return Ok(company);
            else
                return NotFound();
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetByIsin/{isin}")]
    public async Task<IActionResult> GetByIsin(string isin)
    {
        try
        {
            var company = await _repository.GetByIsin(isin);

            if (company != null)
                return Ok(company);
            else
                return NotFound();
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CompanyViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var company = await _repository.GetByIsin(viewModel.Isin);

            if (company != null)
                return BadRequest(new { code = "ISIN", error = "The ISIN already exists!" });

            var id = await _repository.Create(_mapper.Map<Company>(viewModel));

            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(int id, CompanyViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != viewModel.Id)
            return BadRequest(new { code = "ID", error = "The Ids are not equal!" });

        var companyByIsin = await _repository.GetByIsin(viewModel.Isin);
        if (companyByIsin != null && companyByIsin.Id != id)
            return BadRequest(new { code = "ISIN", error = "The ISIN already exists!" });

        try
        {
            var result = await _repository.Update(_mapper.Map<Company>(viewModel));

            if(result)
                return Ok();
            else
                return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _repository.Delete(id);
            
            if(result)
                return Ok();
            else
                return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
