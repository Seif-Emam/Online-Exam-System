using MediatR;

namespace Online_Exam_System.Features.Exam.AddExam
{
    public record AddExamCommend (Guid DiplomaId , string Title, TimeOnly Duration, DateOnly StartDate, DateOnly EndDate, string? PictureUrl) :IRequest<AddExamDTO>;
    
}
