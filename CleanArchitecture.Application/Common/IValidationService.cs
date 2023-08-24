namespace CleanArchitecture.Application.Common
{
    public interface IValidationService
    {
        Task<bool> IsValidNote(int Id);
    }
}
