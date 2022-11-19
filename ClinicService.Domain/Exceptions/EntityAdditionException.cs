namespace ClinicService.Domain.Exceptions;

public sealed class EntityAdditionException : ExceptionBase
{
    public EntityAdditionException(Exception? innerException = null) : base("Entity addition error.", innerException)
    {
        SetErrorCode(Exceptions.ErrorCode.EntityAdditionError);
    }
}
