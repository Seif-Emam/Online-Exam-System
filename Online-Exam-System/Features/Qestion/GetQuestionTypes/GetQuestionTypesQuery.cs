
using MediatR;

namespace Online_Exam_System.Features.Qestion.GetQuestionTypes
{
    public record GetQuestionTypesQuery : IRequest<List<QuestionTypeDto>>;
    
}
