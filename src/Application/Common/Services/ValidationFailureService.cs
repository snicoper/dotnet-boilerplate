using DotnetBoilerplate.Application.Common.Exceptions;
using DotnetBoilerplate.Application.Common.Interfaces;
using FluentValidation.Results;

namespace DotnetBoilerplate.Application.Common.Services;

public class ValidationFailureService : IValidationFailureService
{
    public ValidationFailureService()
    {
        Errors = new List<ValidationFailure>();
    }

    private List<ValidationFailure> Errors { get; }

    public bool HasErrors()
    {
        return Errors.Any();
    }

    public int ErrorsCount()
    {
        return Errors.Count;
    }

    public void Add(string property, string error)
    {
        Errors.Add(new ValidationFailure(property, error));
    }

    public void Add(Dictionary<string, string> errors)
    {
        foreach (var (key, value) in errors)
        {
            Add(key, value);
        }
    }

    public void AddAndRaiseException(string key, string value)
    {
        Add(key, value);
        RaiseException();
    }

    public void AddAndRaiseException(Dictionary<string, string> errors)
    {
        foreach (var (key, value) in errors)
        {
            Add(key, value);
        }

        RaiseException();
    }

    public void RaiseException()
    {
        throw new CustomValidationException(Errors);
    }

    public void RaiseExceptionIfExistsErrors()
    {
        if (HasErrors())
        {
            throw new CustomValidationException(Errors);
        }
    }
}
