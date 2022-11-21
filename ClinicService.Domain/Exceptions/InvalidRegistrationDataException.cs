namespace ClinicService.Domain.Exceptions;

public sealed class InvalidRegistrationDataException : ExceptionBase
{
    public InvalidRegistrationDataException(Exception? innerException = null) : base("Registration data invalid." ,innerException)
    {
        SetErrorCode(Exceptions.ErrorCode.EntityNotFoundError);
    }
}