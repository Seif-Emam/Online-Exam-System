using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.GetById;
using Online_Exam_System.Features.Exam.UpdateExam;
using Online_Exam_System.Services;

namespace Online_Exam_System.Features.Exam.DeleteExam
{
    public class DeleteExamOrchestrator(IMediator mediator, IImageHelper imageHelper) : IDeleteExamCommandOrchestrator
    {
        public async Task<bool> DeleteExamAsync(Guid id)
        {
            var Exam = await mediator.Send(new GetExamByIdQuerey(id));
            if (Exam == null)
                return false;

            var isDeleted = await mediator.Send(new DeleteExamCommend(id));
            if (isDeleted)
                imageHelper.DeleteImage(Exam.PictureUrl);

            return isDeleted;
        }
    }
}
