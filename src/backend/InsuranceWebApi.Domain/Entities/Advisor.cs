using FluentValidation;
using InsuranceWebApi.Domain.Entities.Abstracts;
using InsuranceWebApi.Domain.Entities.Enums;

namespace InsuranceWebApi.Domain.Entities;

public class Advisor : AbstractEntity
{
    public Advisor(string name, string sin, string address, string phone)
    {
        Sin = sin;
        Update(name, address, phone);

        var random = new Random();
        HeathStatus = (double)random.NextDouble() switch
        {
            var r when r < 0.6 => HeathStatusEnum.Green,
            var r when r < 0.8 => HeathStatusEnum.Yellow,
            _ => HeathStatusEnum.Red,
        };
    }

    public void Update(string name, string address, string phone)
    {
        Name = name;
       
        Address = address;
        Phone = phone;
    }

    protected Advisor() { }
    public string Sin { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public HeathStatusEnum HeathStatus { get; private set; }
}

public class AdvisorValidator : AbstractValidator<Advisor>
{
    public AdvisorValidator()
    {
        RuleFor(entity => entity.Name)
             .NotEmpty().WithMessage("Name must be informed.")
             .MaximumLength(255).WithMessage("Name must have at maximum 255 characters.");

        RuleFor(entity => entity.Sin)
            .NotEmpty().WithMessage("SIN must be informed.")
            .Length(9).WithMessage("SIN must have 9 characters.");

        RuleFor(entity => entity.Address)
            .NotEmpty().WithMessage("Address must be informed.")
            .MaximumLength(255).WithMessage("Address must have at maximum 255 characters.");

        When(entity => !string.IsNullOrEmpty(entity.Phone), () =>
        {
            RuleFor(entity => entity.Phone).Length(8).WithMessage("Phone must have 8 characters.");
        });

    }
}
