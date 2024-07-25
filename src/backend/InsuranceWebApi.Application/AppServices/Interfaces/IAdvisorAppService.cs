using InsuranceWebApi.Application.AppServices.Results;
using InsuranceWebApi.Application.Commands.Requests;

namespace InsuranceWebApi.Application.AppServices.Interfaces;

public interface IAdvisorAppService
{
    public CommandResult Create(AdvisorCreateUpdateRequest request);

    public CommandResult Delete(string sin);

    public CommandResult Update(string sin, AdvisorCreateUpdateRequest request);
}
