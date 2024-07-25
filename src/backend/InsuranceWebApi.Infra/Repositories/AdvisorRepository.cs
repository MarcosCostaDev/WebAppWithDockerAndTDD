using InsuranceWebApi.Domain.Entities;
using InsuranceWebApi.Domain.Repositories;
using InsuranceWebApi.Infra.Contexts;

namespace InsuranceWebApi.Infra.Repositories;

public class AdvisorRepository(CacheContext<Advisor> dbContext) : IAdvisorRepository
{
    public Advisor CreateEdit(Advisor advisor)
    {
        dbContext.Put(advisor.Sin, advisor);
        return advisor;
    }

    public void Delete(Advisor advisor)
    {
        dbContext.Delete(advisor.Sin);
    }

    public void Delete(string sin)
    {
        Delete(GetAdvisorBySin(sin)!);
    }

    public Advisor? GetAdvisorBySin(string sin)
    {
        return dbContext.Get(sin);
    }
    public IEnumerable<Advisor> GetAdvisors()
    {
        return dbContext.Get();
    }

    public IEnumerable<Advisor> GetAdvisors(string? name)
    {
        return dbContext.Get().Where(p => name == null || p.Name.Contains(name));
    }
}
