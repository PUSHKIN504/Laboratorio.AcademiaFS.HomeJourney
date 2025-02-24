namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    public interface IUnitOfWork
    {
        int Save();
        Task<int> SaveAsync();
    }
}
