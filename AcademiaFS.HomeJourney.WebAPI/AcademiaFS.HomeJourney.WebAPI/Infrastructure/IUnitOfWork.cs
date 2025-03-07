﻿using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney.Entities;

namespace AcademiaFS.HomeJourney.WebAPI.Infrastructure
{
    public interface IUnitOfWork
    {
        //int Save();
        //Task<int> SaveAsync();
        //void BeginTransaction();
        //Task BeginTransactionAsync();
        //void CommitTransaction();
        //Task CommitTransactionAsync();
        //void RollbackTransaction();
        //Task RollbackTransactionAsync();
        //Task AddRangeAsync(List<Viajes> trips);
        HomeJourneyContext Context { get; } // Exponer el contexto para acceder a DbSets
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
