using AcademiaFS.HomeJourney.WebAPI.Infrastructure.HomeJourney;

namespace AcademiaFS.HomeJourney.WebAPI._Features
{
    public interface IGenericServiceInterface<TEntity, TKey> where TEntity : class, IActivableInterface
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(TKey id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void SetActive(TKey id, bool active);
        //void Save();
    }
}
