using InsuranceWebApi.Application.AppServices.Interfaces;
using InsuranceWebApi.Application.AppServices.Results;
using InsuranceWebApi.Application.Commands.Requests;
using InsuranceWebApi.Application.Extensions;
using InsuranceWebApi.Domain.Entities;
using InsuranceWebApi.Domain.Repositories;

namespace InsuranceWebApi.Application.AppServices;

public class AdvisorAppService : IAdvisorAppService
{
    private readonly IAdvisorRepository _advisorRepository;

    public AdvisorAppService(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }

    public CommandResult Create(AdvisorCreateUpdateRequest request)
    {
        var advisor = new Advisor(request.Name, request.Sin, request.Address!, request.Phone);

        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        if (!result.IsValid) return advisor.ToCommandResult(result);

        _advisorRepository.CreateEdit(advisor);

        return advisor.ToCommandResult();
    }

    public CommandResult Delete(string sin)
    {
        var advisor = _advisorRepository.GetAdvisorBySin(sin);

        if (advisor == null) return CommandResultExtension.NotFoundMessage();

        _advisorRepository.Delete(advisor);

        return CommandResultExtension.EmptySuccessMessage();
    }

    public CommandResult Update(string sin, AdvisorCreateUpdateRequest request)
    {
        var advisor = _advisorRepository.GetAdvisorBySin(sin);

        if (advisor == null) return CommandResultExtension.NotFoundMessage();

        advisor.Update(request.Name, request.Address!, request.Phone);

        var validator = new AdvisorValidator();

        var result = validator.Validate(advisor);

        if(!result.IsValid) return advisor.ToCommandResult(result);

        _advisorRepository.CreateEdit(advisor);

        return advisor.ToCommandResult();
    }
}
