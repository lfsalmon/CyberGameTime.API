using CyberGameTime.Entities.Common;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CyberGameTime.Application.Repositories.Common;


public interface IGenericRepository<TEntity>  where TEntity : IEntity
{
    IEnumerable<TEntity> GetAll(bool is_tracking = false);
    TEntity? FindById(long id, bool is_tracking = false);
    IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate, bool is_tracking = false, params string[] includePaths); // nuevo método genérico
    Task<TEntity> Add(TEntity _entity);
    Task<bool> Update(TEntity _entity);
    Task<bool> Delete(long  id);

}
