using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using CyberGameTime.Entities.Common;

namespace CyberGameTime.Application.Repositories.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class,IEntity
    {
        
        private readonly ILogger<GenericRepository<TEntity>> _logger;
        protected readonly CyberGameContext _context;


        public GenericRepository(ILogger<GenericRepository<TEntity>> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var _aux = new DbContextOptionsBuilder<CyberGameContext>();
            _aux.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            _context = new CyberGameContext(_aux.Options);

        }
        private DbSet<TEntity> getEntity() 
        {
            var _db_set = _context.Set<TEntity>();
            if (_db_set is null)
                throw new InvalidOperationException($"El tipo {typeof(TEntity).Name} no está configurado en el contexto.");
            return _db_set;
        }

        public IEnumerable<TEntity> GetAll( bool is_tracking = false)
        {
            var dbSet = getEntity();
            return is_tracking
                    ? dbSet?.AsTracking().ToList()
                    : dbSet?.AsNoTracking().ToList();
        }

        public async Task<TEntity> Add(TEntity _entity)
        {
            var _db_set = getEntity();
            await _db_set.AddAsync(_entity);
            _context.SaveChanges();
            return _entity;
        }

        public TEntity? FindById(long id, bool is_tracking=false)
        {
            var dbSet = getEntity();
            return is_tracking
                    ? dbSet?.AsTracking().FirstOrDefault(x => x.Id == id)
                    : dbSet?.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Delete(long id)
        {
            var _entity=FindById(id, true);
            if(_entity is null)
                throw new KeyNotFoundException($"El id {id} no se encuentra en la tabla {typeof(TEntity).Name}");
            getEntity().Remove(_entity);
            return _context.SaveChanges()>0;
        }

        public async Task<bool> Update(TEntity _entity)
        {
            getEntity().Update(_entity);
            return _context.SaveChanges() > 0;
        }
    }
}
