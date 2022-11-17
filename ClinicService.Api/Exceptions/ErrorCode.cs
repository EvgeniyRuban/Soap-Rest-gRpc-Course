namespace ClinicService.Api.Exceptions;

public enum ErrorCode
{
    ServerSideError = 1000,
    EntityNotFound = 1101,
    EntityReceivingError = 1100,
    EntityAdditionError = 1200,
    EntityUpdatingError = 1300,
    EntityDeletionError = 1400,
}