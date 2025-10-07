using System.Linq.Expressions;
using Online_Exam_System.Models;

namespace Online_Exam_System.Contarcts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll(bool trackChanges = false);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);
        Task<TEntity?> GetByIdAsync(Guid id);

        
    }
}
