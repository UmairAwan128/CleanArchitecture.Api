namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IValidationService
    {
        Task<bool> IsValidNote(int Id);
    }
}
