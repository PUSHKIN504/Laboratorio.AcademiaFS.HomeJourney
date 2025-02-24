using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HomeJourneyContext _context;
        private bool _disposed;

        public UnitOfWork(HomeJourneyContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
