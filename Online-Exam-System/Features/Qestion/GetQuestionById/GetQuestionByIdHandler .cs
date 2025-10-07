using MediatR;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Models.Questions;


namespace Online_Exam_System.Features.Qestion.GetQuestionById
{
    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
            public async Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == Guid.Empty)
                    throw new ArgumentException("Invalid question Id.");

                var repo = _unitOfWork.GetRepository<Question>();

                var question = await repo
                    .GetAll()
                    .Include(q => q.Exam)
                    .Include(q => q.Choices)
                    .FirstOrDefaultAsync(q => q.Id == request.Id, cancellationToken);

                if (question == null)
                    throw new KeyNotFoundException("Question not found.");

                // 🧠 ممكن تضيف caching هنا لاحقاً (MemoryCache أو Redis)

                return new QuestionDto
                {
                    Id = question.Id,
                    QuestionText = question.Title,
                    Type = question.Type,
                    ExamName = question.Exam?.Title,
                    Choices = question.Choices.Select(c => new ChoiceDto
                    {
                        Id = c.Id,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error fetching question details: {ex.Message}");
            }
        }
    }
}