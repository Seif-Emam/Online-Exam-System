using MediatR;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Qestion.GetAllQuestions
{
    public class GetAllQuestionsQuery : IRequest<PagedResult<QuestionDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? ExamName { get; set; }
    }
}
