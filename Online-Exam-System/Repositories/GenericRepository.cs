using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Data;
using Online_Exam_System.Models;

namespace Online_Exam_System.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly OnlineExamContext _onlineExamContext;
        public GenericRepository( OnlineExamContext onlineExamContext)
        {
            _onlineExamContext = onlineExamContext;
        }   

        public async Task CreateAsync(TEntity entity)
            => await _onlineExamContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
       => _onlineExamContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
          => _onlineExamContext.Set<TEntity>().Update(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
            => TrackChanges ? await _onlineExamContext.Set<TEntity>().ToListAsync() 
            : await _onlineExamContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id)
        
             => await  _onlineExamContext.Set<TEntity>().FindAsync(id).AsTask();
        

       

       
    }
}
