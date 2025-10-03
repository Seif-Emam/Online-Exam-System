using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Exam.DeleteExam
{
    public class DeleteExamCommendHandler
        (IUnitOfWork _unitOfWork) : IRequestHandler<DeleteExamCommend, bool>
    {
        public async Task<bool> Handle(DeleteExamCommend request, CancellationToken cancellationToken)
        {
            try
            {
                var examRepo = _unitOfWork.GetRepository<Models.Exam>();
                var exam = await examRepo.GetByIdAsync(request.id);

                if (exam == null)
                    return false;

                examRepo.Delete(exam);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                
                return false; 
            }
        }
    }
}
