using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Exam.AddExam
{
    public class AddExamHandler(IUnitOfWork unitOfWork ) : IRequestHandler<AddExamCommend, AddExamDTO>
    {
        public async Task<AddExamDTO> Handle(AddExamCommend request, CancellationToken cancellationToken)
        {
            var rebo = unitOfWork.GetRepository<Models.Exam>();
            var exam = new Models.Exam
            {
                Title = request.Title,
                Duration = request.Duration,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PictureUrl = request.PictureUrl
                
            };
            await rebo.CreateAsync(exam);
            await unitOfWork.SaveChangesAsync();
            return new AddExamDTO
            {
                Id = exam.Id,
                Title = exam.Title,
                PictureUrl = exam.PictureUrl,
                StartDate = exam.StartDate,
                EndDate = exam.EndDate,
                Duration = exam.Duration
            };
        }
    }
}
