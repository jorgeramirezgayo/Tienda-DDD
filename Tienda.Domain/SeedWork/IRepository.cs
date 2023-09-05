namespace Tienda.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T entity);
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
