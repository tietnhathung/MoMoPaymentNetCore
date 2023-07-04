using System.Runtime.Serialization;
using MoMoSdk.Models;

namespace MoMoSdk.Exceptions;

public class MoMoException: Exception
{
    public MoMoResponse Response { set; get; }
    
    public MoMoException()
    {
    }

    protected MoMoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public MoMoException(string? message) : base(message)
    {
    }

    public MoMoException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    public MoMoException(MoMoResponse response)
    {
        Response = response;
    }

    protected MoMoException(SerializationInfo info, StreamingContext context,MoMoResponse response) : base(info, context)
    {
        Response = response;
    }

    public MoMoException(string? message,MoMoResponse response) : base(message)
    {
        Response = response;
    }

    public MoMoException(string? message, Exception? innerException,MoMoResponse response) : base(message, innerException)
    {
        Response = response;
    }
}