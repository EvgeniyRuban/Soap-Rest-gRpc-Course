namespace ClinicService.Api.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
    protected ExceptionBase() : base("Server side error.")
    {
    }

    public virtual ErrorCode ErrorCode { get; protected set; } = ErrorCode.ServerSideError;
}