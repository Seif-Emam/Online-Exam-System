using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Data;
using Online_Exam_System.Models;
using System.Linq.Expressions;

namespace Online_Exam_System.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OnlineExamContext _onlineExamContext;

        public GenericRepository(OnlineExamContext onlineExamContext)
        {
            _onlineExamContext = onlineExamContext;
        }

        public async Task CreateAsync(TEntity entity)
            => await _onlineExamContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;
            _onlineExamContext.Set<TEntity>().Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _onlineExamContext.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = false)
        {
            var query = _onlineExamContext.Set<TEntity>()
                .Where(e => !e.IsDeleted)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
        {
            var query = _onlineExamContext.Set<TEntity>()
                .Where(expression)
                .Where(e => !e.IsDeleted)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await _onlineExamContext.Set<TEntity>().FindAsync(id);
            return entity is not null && !entity.IsDeleted ? entity : null;
        }
    }
}
