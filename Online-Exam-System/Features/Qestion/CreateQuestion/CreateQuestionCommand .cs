using MediatR;
using Online_Exam_System.Features.Qestion.GetAllQuestions;

namespace Online_Exam_System.Features.Qestion.CreateQuestion
{
    public record CreateQuestionCommand(string Title , string Type, Guid ExamId , List<CreateChoiceDto> Choices) : IRequest<QuestionDto>;


}
