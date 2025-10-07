namespace ItiProject_ms1.Repository
{
    public interface IWritableRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
