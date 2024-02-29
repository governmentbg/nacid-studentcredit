namespace StudentCredit.Application.DomainValidation.Enums
{
    public enum ApplicationFourErrorCode
    {
        ApplicationFourTypeEmpty = 401,
        BankAccountEmpty = 402,
        ValueMustBeGreaterThanZero = 403,
        InvalidFourDescription = 404,
        MissingRequiredAttachedFile = 405
    }
}
