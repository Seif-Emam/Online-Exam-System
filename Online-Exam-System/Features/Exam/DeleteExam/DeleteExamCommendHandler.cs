using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Exam.DeleteExam
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommend, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public DeleteExamCommandHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<bool> Handle(DeleteExamCommend request, CancellationToken cancellationToken)
        {
            var examRepo = _unitOfWork.GetRepository<Models.Exam>();
            var exam = await examRepo.GetByIdAsync(request.id);

            if (exam is null)
                throw new KeyNotFoundException("Exam not found.");

            try
            {
                examRepo.Delete(exam);
                var result = await _unitOfWork.SaveChangesAsync() > 0;

                // 🧹 clear cache to reload fresh data next time
                _cache.Remove("AllExams");

                return result;
            }
            catch (Exception ex)
            {
                // middleware هيشوفه ويعمله handle تلقائي
                throw new ApplicationException("An error occurred while deleting the exam.", ex);
            }
        }
    }
}
