using MediatR;

namespace Online_Exam_System.Features.Qestion.GetQuestionTypes
{
    public class GetQuestionTypesHandler : IRequestHandler<GetQuestionTypesQuery, List<QuestionTypeDto>>
    {
        public async Task<List<QuestionTypeDto>> Handle(GetQuestionTypesQuery request, CancellationToken cancellationToken)
        {
            var questionTypes = new List<QuestionTypeDto>
            {
                new QuestionTypeDto { Id = 1, Name = "Single Choice" },
                new QuestionTypeDto { Id = 2, Name = "Multiple Choice" },
                new QuestionTypeDto { Id = 3, Name = "Image Choice" }
            };

            return await Task.FromResult(questionTypes);
        }
    }
}
