using AcademiaFS.HomeJourney.WebAPI.Infrastructure;
using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFS.HomeJourney.WebAPI._Features
{

        public class GenericService<TEntity, TKey> : IGenericServiceInterface<TEntity, TKey> where TEntity : class, IActivableInterface
        {
            private readonly HomeJourneyContext _context;
            private readonly IUnitOfWork _unitOfWork;

        public GenericService(HomeJourneyContext context, IUnitOfWork unitOfWork)
            {
                _context = context;
                _unitOfWork = unitOfWork;
                
            }

            public IEnumerable<TEntity> GetAll()
            {
                return _context.Set<TEntity>().AsNoTracking().ToList();
            }

            public TEntity? GetById(TKey id)
            {
                return _context.Set<TEntity>().Find(id);
            }

            public TEntity Create(TEntity entity)
            {
                _context.Set<TEntity>().Add(entity);
                _unitOfWork.Save();
            return entity;
            }

            public TEntity Update(TEntity entity)
            {
                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Save();
            return entity;
            }

            public void SetActive(TKey id, bool active)
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    throw new Exception("Entidad no encontrada");
                }
                entity.Activo = active;
                _unitOfWork.Save();
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
    
}
