namespace ClinicService.Api.Exceptions;

public sealed class EntityUpdatingException : ExceptionBase
{
    public EntityUpdatingException(Exception? innerException = null) : base("Entity updating error.", innerException)
    {
        SetErrorCode(Exceptions.ErrorCode.EntityUpdatingError);
    }
}