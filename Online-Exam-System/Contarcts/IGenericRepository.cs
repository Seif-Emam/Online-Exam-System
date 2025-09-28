using Online_Exam_System.Models;

namespace Online_Exam_System.Contarcts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges=false);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
