using InsuranceWebApi.Application.AppServices.Interfaces;
using InsuranceWebApi.Application.Commands.Requests;
using InsuranceWebApi.Application.Extensions;
using InsuranceWebApi.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvisorController : ControllerBase
{
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IAdvisorAppService _advisorAppService;

    public AdvisorController(IAdvisorRepository advisorRepository, IAdvisorAppService advisorAppService) {
        _advisorRepository = advisorRepository;
        _advisorAppService = advisorAppService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery(Name = "name")]string? name) 
    {
        var list = _advisorRepository.GetAdvisors(name);
        return Ok(list.ToCommandResult());
    }


    [HttpGet("{sin}")]
    public IActionResult GetbyId([FromRoute(Name = "sin")] string sin)
    {
        var item = _advisorRepository.GetAdvisorBySin(sin);

        if (item is null) return NotFound();

        return Ok(item.ToCommandResult());
    }

    [HttpPost]
    public IActionResult Create([FromBody] AdvisorCreateUpdateRequest request)
    {
        var result = _advisorAppService.Create(request);

        if (!result.Valid) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{sin}")]
    public IActionResult Update([FromRoute(Name = "sin")] string sin, [FromBody] AdvisorCreateUpdateRequest request)
    {
        var result = _advisorAppService.Update(sin, request);

        if(!result.Valid) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{sin}")]
    public IActionResult Delete([FromRoute(Name = "sin")] string sin)
    {
        var result = _advisorAppService.Delete(sin);

        if (!result.Valid) return BadRequest(result);

        return Ok(result);
    }
}
