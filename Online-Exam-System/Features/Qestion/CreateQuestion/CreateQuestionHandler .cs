using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Features.Qestion.CreateQuestion
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questionRepo = _unitOfWork.GetRepository<Question>();
                var choiceRepo = _unitOfWork.GetRepository<Choice>();

                
                var question = new Question
                {
                    Title = request.Title,
                    Type = request.Type,
                    ExamId = request.ExamId,
                    CreatedAt = DateTime.UtcNow,
                };

                await questionRepo.CreateAsync(question);
                await _unitOfWork.SaveChangesAsync();

              
                var savedChoices = new List<Choice>();

                foreach (var choiceDto in request.Choices)
                {
                    var choice = new Choice
                    {
                        Content = choiceDto.Content,
                        IsCorrect = choiceDto.IsCorrect,
                        QuestionId = question.Id
                    };

                    await choiceRepo.CreateAsync(choice);
                    savedChoices.Add(choice);
                }

                await _unitOfWork.SaveChangesAsync();

               
                return new QuestionDto
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type,
                    Choices = savedChoices.Select(c => new ChoiceDto
                    {
                        Id = c.Id,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new ApplicationException($"Error while creating question: {innerMessage}");
            }
        }

    }
}
