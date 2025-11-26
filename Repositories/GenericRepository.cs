using FormBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FormBackend.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Get all records
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Get a record by ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Find records by condition
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Add a new record
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Update an existing record
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Remove a record
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);

        }
    }
}
