using InsuranceWebApi.Domain.Entities;

namespace InsuranceWebApi.Domain.Repositories;

public interface IAdvisorRepository
{

    public Advisor CreateEdit(Advisor advisor);
    public void Delete(Advisor advisor);
    public void Delete(string sin);
    public Advisor? GetAdvisorBySin(string sin);
    public IEnumerable<Advisor> GetAdvisors();
    public IEnumerable<Advisor> GetAdvisors(string? name);

}
