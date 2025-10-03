using MediatR;

namespace Online_Exam_System.Features.Exam.AddExam
{
    public record AddExamCommend(string Title, TimeOnly Duration, DateOnly StartDate, DateOnly EndDate, string? PictureUrl) :IRequest<AddExamDTO>;
    
}
