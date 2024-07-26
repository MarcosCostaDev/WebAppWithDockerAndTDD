namespace InsuranceWebApi.Application.Commands.Requests;

public class AdvisorCreateUpdateRequest
{
    public string Name { get; set; }
    public string Sin { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
