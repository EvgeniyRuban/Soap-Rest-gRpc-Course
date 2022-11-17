namespace ClinicService.Api.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
    protected ExceptionBase(Exception? innerException = null) : base("Server side error.", innerException)
    {
    }

    public virtual int ErrorCode { get; private set; } = (int)Exceptions.ErrorCode.ServerSideError;

    protected void SetErrorCode(ErrorCode errorCode) => ErrorCode = (int)errorCode;
}