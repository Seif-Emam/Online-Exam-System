using Online_Exam_System.Models;

namespace Online_Exam_System.Contarcts
{
    public interface IUnitOfWork
    {
        public Task <int> SaveChangesAsync();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
