using System.Runtime.Serialization;

namespace DotnetBoilerplate.Application.Common.EntityFramework.OrderBy.Exceptions;

[Serializable]
public class OrderFieldEntityNotFoundException : Exception
{
    public OrderFieldEntityNotFoundException(string name, object key)
        : base($@"Entity ""{name}"" ({key}) was not found for ordering.")
    {
    }

    protected OrderFieldEntityNotFoundException(
        SerializationInfo serializationInfo,
        StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
