using Online_Exam_System.Contarcts;
using Online_Exam_System.Data;
using Online_Exam_System.Models;
using System.Collections.Concurrent;

namespace Online_Exam_System.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineExamContext _onlineExamContext;
        private readonly ConcurrentDictionary<string, object> _Repositories;

        public UnitOfWork(OnlineExamContext onlineExamContext) {
            _onlineExamContext = onlineExamContext;
            _Repositories = new ConcurrentDictionary<string, object>();
        } 
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            return (IGenericRepository<TEntity>)_Repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity>(_onlineExamContext));
        }

        public async Task<int> SaveChangesAsync()
        
            => await _onlineExamContext.SaveChangesAsync();

    }
}
