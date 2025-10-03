using MediatR;

namespace Online_Exam_System.Features.Exam.DeleteExam
{
    public record DeleteExamCommend(Guid id) : IRequest<bool>;
  
}
