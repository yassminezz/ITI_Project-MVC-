using ItiProject_ms1.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiProject_ms1.Repository
{
    public class BaseRepository<T>:IBaseRepository<T> where T : class
    {
        private readonly UniDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(UniDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll() => _dbSet.ToList();

        public T GetByID(int id) => _dbSet.Find(id);

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            Save();
        }

        public void Save() => _context.SaveChanges();
    }
}

