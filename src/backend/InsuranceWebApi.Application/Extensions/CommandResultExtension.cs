
using FluentValidation.Results;
using InsuranceWebApi.Application.AppServices.Results;

namespace InsuranceWebApi.Application.Extensions;
public static class CommandResultExtension
{
    public static CommandResult ToCommandResult(this object data, ValidationResult? validation = null)
    {
        return new CommandResult(data, validation!);
    }

    public static CommandResult NotFoundMessage(string? property = null, string? message = null)
    {
        return new CommandResult(null!, new ValidationResult
        {
            Errors =
            [
               new ValidationFailure(property ?? "Request", message ?? "The record was not found.")
            ]
        });
    }

    public static CommandResult EmptySuccessMessage()
    {
        return new CommandResult(null!, null!);
    }
}
