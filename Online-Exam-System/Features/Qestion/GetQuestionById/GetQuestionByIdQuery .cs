using MediatR;

namespace Online_Exam_System.Features.Qestion.GetQuestionById
{
    public record GetQuestionByIdQuery(Guid Id) : IRequest<QuestionDto>
    {
    }
}
