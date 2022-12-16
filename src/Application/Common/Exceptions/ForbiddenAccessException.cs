using System.Runtime.Serialization;

namespace DotnetBoilerplate.Application.Common.Exceptions;

[Serializable]
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }

    protected ForbiddenAccessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
