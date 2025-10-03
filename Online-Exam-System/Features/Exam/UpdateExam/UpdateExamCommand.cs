using MediatR;

namespace Online_Exam_System.Features.Exam.UpdateExam
{
    public record UpdateExamCommand(Guid id ,string Title, TimeOnly Duration, DateOnly StartDate, DateOnly EndDate, string? PictureUrl) :IRequest<UpdateExamDTO>;


}
