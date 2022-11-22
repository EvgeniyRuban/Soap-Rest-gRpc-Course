namespace ClinicService.Domain.Exceptions;

public sealed class EntityDeletionException : ExceptionBase
{
    public EntityDeletionException(Exception? innerException = null) : base("Entity deletion error.", innerException)
    {
        SetErrorCode(Exceptions.ErrorCode.EntityDeletionError);
    }
}