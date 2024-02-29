namespace StudentCredit.Infrastructure.DomainValidation.Enums
{
    public enum StudentErrorCode
    {
        StudentNotFoundWithGivenIdentificator = 401,
        StudentNotFoundWithGivenUan = 402,
        DesyncedUanBetweenSystems = 403
    }
}
