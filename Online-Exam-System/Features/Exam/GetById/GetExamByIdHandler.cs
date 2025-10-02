using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Exam.GetById
{
    public class GetExamByIdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetExamByIdQuerey, GetExamsByIdDTOs>
    {
        public async Task<GetExamsByIdDTOs> Handle(GetExamByIdQuerey request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.GetRepository<Models.Exam>().GetByIdAsync(request.Id);
            if (exam == null)
                return null!;
            return new GetExamsByIdDTOs
            {
               Id = exam.Id,
               Title = exam.Title,
               PictureUrl = exam.PictureUrl,
               StartDate = exam.StartDate,
               EndDate = exam.EndDate,
               Duration = exam.duration


            };

        }
    }
}
