namespace ClinicService.Api.Exceptions;

public sealed class EntityDeletionException : ExceptionBase
{
    public EntityDeletionException(Exception? innerException = null) : base("Entity deletion error.", innerException)
    {
        ErrorCode = ErrorCode.EntityDeletionError;
    }
}