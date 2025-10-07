namespace ItiProject_ms1.Repository
{
    public interface IBaseRepository<T> : IReadableRepository<T>, IWritableRepository<T> where T : class
    {

    }
}
