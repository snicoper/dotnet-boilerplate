using FluentValidation.Results;
using System.Runtime.Serialization;

namespace DotnetBoilerplate.Application.Common.Exceptions;

[Serializable]
public class CustomValidationException : Exception
{
    public CustomValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    protected CustomValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }

    public IDictionary<string, string[]> Errors { get; } = null!;
}
