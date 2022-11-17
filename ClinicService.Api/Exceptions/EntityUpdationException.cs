namespace ClinicService.Api.Exceptions;

public sealed class EntityUpdationException : ExceptionBase
{
    public EntityUpdationException(Exception? innerException = null) : base("Entity updating error.", innerException)
    {
        ErrorCode = ErrorCode.EntityUpdatingError;
    }
}
