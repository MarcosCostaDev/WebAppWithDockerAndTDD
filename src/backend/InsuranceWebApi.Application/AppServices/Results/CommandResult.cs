
using FluentValidation.Results;

namespace InsuranceWebApi.Application.AppServices.Results;

public class CommandResult<T>
{
    public CommandResult(T data, ValidationResult validation)
    {
        if (validation != null && !validation.IsValid)
        {
            Errors = validation.Errors.Select(p => p.ErrorMessage);
            Valid = validation.IsValid;
            return;
        }
        Data = data;
        Valid = true;
    }

    public bool Valid { get; set; }
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; }
}


public class CommandResult : CommandResult<object>
{
    public CommandResult(object data, ValidationResult validation) : base(data, validation)
    {
    }
}