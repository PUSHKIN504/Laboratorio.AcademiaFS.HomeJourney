using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly HomeJourneyContext _context;
    //    private IDbContextTransaction _transaction;
    //    private bool _disposed;

    //    public UnitOfWork(HomeJourneyContext context)
    //    {
    //        _context = context;
    //    }


    //    public int Save()
    //    {
    //        return _context.SaveChanges();
    //    }

    //    public async Task<int> SaveAsync()
    //    {
    //        return await _context.SaveChangesAsync();
    //    }

    //    public void BeginTransaction()
    //    {
    //        if (_transaction == null)
    //            _transaction = _context.Database.BeginTransaction();
    //    }

    //    public async Task BeginTransactionAsync()
    //    {
    //        if (_transaction == null)
    //            _transaction = await _context.Database.BeginTransactionAsync();
    //    }


    //    public void CommitTransaction()
    //    {
    //        try
    //        {
    //            Save();
    //            _transaction?.Commit();
    //        }
    //        catch
    //        {
    //            RollbackTransaction();
    //            throw;
    //        }
    //        finally
    //        {
    //            DisposeTransaction();
    //        }
    //    }

    //    public async Task CommitTransactionAsync()
    //    {
    //        try
    //        {
    //            await SaveAsync();
    //            if (_transaction != null)
    //                await _transaction.CommitAsync();
    //        }
    //        catch
    //        {
    //            await RollbackTransactionAsync();
    //            throw;
    //        }
    //        finally
    //        {
    //            DisposeTransaction();
    //        }
    //    }

    //    public Task AddRangeAsync(List<Viajes> trips)
    //    {
    //        return Task.CompletedTask;
    //    }

    //    public void RollbackTransaction()
    //    {
    //        _transaction?.Rollback();
    //        DisposeTransaction();
    //    }

    //    public async Task RollbackTransactionAsync()
    //    {
    //        if (_transaction != null)
    //            await _transaction.RollbackAsync();
    //        DisposeTransaction();
    //    }

    //    private void DisposeTransaction()
    //    {
    //        if (_transaction != null)
    //        {
    //            _transaction.Dispose();
    //            _transaction = null;
    //        }
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!_disposed)
    //        {
    //            if (disposing)
    //            {
    //                DisposeTransaction();
    //                _context.Dispose();
    //            }
    //            _disposed = true;
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //}
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HomeJourneyContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(HomeJourneyContext context)
        {
            _context = context;
        }

        public HomeJourneyContext Context => _context; // Añadir esta propiedad

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            try
            {
                Save();
                _transaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveAsync();
                if (_transaction != null)
                    await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            DisposeTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DisposeTransaction();
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
