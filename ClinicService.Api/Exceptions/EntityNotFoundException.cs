namespace ClinicService.Api.Exceptions;

public sealed class EntityNotFoundException : ExceptionBase
{
    public EntityNotFoundException(Exception? innerException = null) : base("Entity not found.", innerException)
    {
        ErrorCode = ErrorCode.EntityNotFound;
    }
}