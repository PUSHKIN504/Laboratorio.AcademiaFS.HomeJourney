namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    public interface IUnitOfWork
    {
        int Save();
        Task<int> SaveAsync();
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void RollbackTransaction();
        Task RollbackTransactionAsync();
    }
}
