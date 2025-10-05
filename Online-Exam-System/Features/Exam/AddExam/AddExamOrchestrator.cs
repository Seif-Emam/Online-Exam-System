using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Exam.AddExam
{
    public class AddExamOrchestrator(IMediator mediator, IImageHelper imageHelper) : IAddExamOrchestrator
    {
        public async Task<AddExamDTO> AddExamAsync(AddExamRequest request)
        {
            var PicUrl =await imageHelper.SaveImageAsync(request.PictureUrl, "Exams");
            return await mediator.Send(new AddExamCommend(
                request.DiplomaId,
                request.Title,
                request.Duration,
                request.StartDate,
                request.EndDate,
                PicUrl));

        }
    }
}
