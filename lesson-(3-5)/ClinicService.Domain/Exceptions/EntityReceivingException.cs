namespace ClinicService.Domain.Exceptions;

public sealed class EntityReceivingException : ExceptionBase
{
    public EntityReceivingException(Exception? innerException = null) : base("Entity receiving error.", innerException)
    {
        SetErrorCode(Exceptions.ErrorCode.EntityReceivingError);
    }
}