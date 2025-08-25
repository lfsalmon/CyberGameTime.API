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
        protected DbContextOptionsBuilder<CyberGameContext> _DbContextSettigs;

        public GenericRepository(ILogger<GenericRepository<TEntity>> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                connectionString = configuration.GetConnectionString("ConnectionString");

            _DbContextSettigs = new DbContextOptionsBuilder<CyberGameContext>();
            _DbContextSettigs.UseSqlServer(connectionString);
        }

        private CyberGameContext CreateContext() => new CyberGameContext(_DbContextSettigs.Options);

        public IEnumerable<TEntity> GetAll(bool is_tracking = false)
        {
            try
            {
                using var context = CreateContext();
                IQueryable<TEntity> query = context.Set<TEntity>();
                query = is_tracking ? query.AsTracking() : query.AsNoTracking();
                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los registros de {Entity}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                using var context = CreateContext();
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error agregando registro de {Entity}", typeof(TEntity).Name);
                throw;
            }
        }

        public TEntity? FindById(long id, bool is_tracking = false)
        {
            try
            {
                using var context = CreateContext();
                IQueryable<TEntity> query = context.Set<TEntity>();
                query = is_tracking ? query.AsTracking() : query.AsNoTracking();
                return query.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error buscando id {Id} en {Entity}", id, typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                using var context = CreateContext();
                var set = context.Set<TEntity>();
                var entity = await set.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                    throw new KeyNotFoundException($"El id {id} no se encuentra en la tabla {typeof(TEntity).Name}");
                set.Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando id {Id} en {Entity}", id, typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            try
            {
                using var context = CreateContext();
                context.Set<TEntity>().Update(entity);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando registro de {Entity}", typeof(TEntity).Name);
                throw;
            }
        }
    }
}
