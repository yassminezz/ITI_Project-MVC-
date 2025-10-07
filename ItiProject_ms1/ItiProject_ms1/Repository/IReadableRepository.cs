namespace ItiProject_ms1.Repository
{
    public interface IReadableRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByID(int id);
    }
}
