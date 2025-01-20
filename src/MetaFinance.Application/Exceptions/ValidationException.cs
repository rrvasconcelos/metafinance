using System.Runtime.Serialization;
using FluentValidation.Results;

namespace MetaFinance.Application.Exceptions;

[Serializable]
public class ValidationException: Exception
{
    protected ValidationException(
        SerializationInfo info,
        StreamingContext context) 
        : base(info, context)
    {
        Errors = new Dictionary<string, string[]>();
    }
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}