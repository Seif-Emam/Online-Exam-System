using MediatR;
using Online_Exam_System.Features.Qestion.GetAllQuestions;

namespace Online_Exam_System.Features.Qestion.UpdateQustions
{
    public record UpdateQuestionCommand(Guid Id , string Title , string Type , List<ChoiceDto> Choices) : IRequest<QuestionDto>;
}
