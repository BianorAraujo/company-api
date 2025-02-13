using AutoMapper;
using CompanyApi.Business.Interfaces;
using CompanyApi.Business.Models;
using CompanyApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public CompanyController(ILogger<CompanyController> logger, ICompanyRepository repository, IMapper mapper)
    {
        _logger = logger;
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
        if (id != viewModel.Id)
        {
            return BadRequest("The Ids are not equal!");
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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

    private async Task<CompanyViewModel> GetCompany(int id)
    {
        return _mapper.Map<CompanyViewModel>(await _repository.GetById(id));
    }
}
