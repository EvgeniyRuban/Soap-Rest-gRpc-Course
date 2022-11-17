namespace ClinicService.Api.Exceptions;

public sealed class EntityAdditionException : ExceptionBase
{
    public EntityAdditionException(Exception? innerException = null) : base("Entity addition error.", innerException)
    {
        ErrorCode = ErrorCode.EntityAdditionError;
    }
}
