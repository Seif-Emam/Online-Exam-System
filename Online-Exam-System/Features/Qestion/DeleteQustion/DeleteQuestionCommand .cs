using MediatR;

namespace Online_Exam_System.Features.Qestion.DeleteQustion
{
    public record DeleteQuestionCommand (Guid Id) : IRequest<bool>;
}
