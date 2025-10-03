using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.GetById;

namespace Online_Exam_System.Features.Exam.UpdateExam
{
    public class UpdateExamOrchestrator(IMediator mediator, IImageHelper imageHelper) : IUpdateExamOrchestrator
    {
        public async Task<UpdateExamDTO> UpdateExamAsync(Guid id, UpdateExamRequest request)
        {
            var exam = await mediator.Send(new GetExamByIdQuerey(id));
            if (exam == null)
                return null;
            string? pictureUrl = exam.PictureUrl;
            if (request.PictureUrl is not null && request.PictureUrl.Length > 0)
            {
                if (!string.IsNullOrEmpty(exam.PictureUrl))
                    imageHelper.DeleteImage(exam.PictureUrl);

                pictureUrl = await imageHelper.SaveImageAsync(request.PictureUrl, "Exams");
            }

            var updatedExam = await mediator.Send(new UpdateExamCommand(
                 id,
                request.Title,
                request.Duration,
                request.StartDate,
                request.EndDate,
                pictureUrl
            ));

            return updatedExam;

        }
    }
}
