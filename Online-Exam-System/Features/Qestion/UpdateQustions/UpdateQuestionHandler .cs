using MediatR;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Features.Qestion.UpdateQustions
{
    public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionCommand, QuestionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<QuestionDto> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questionRepo = _unitOfWork.GetRepository<Question>();
                var choiceRepo = _unitOfWork.GetRepository<Choice>();

         
                var question = await questionRepo.GetByIdAsync(request.Id);
                if (question == null)
                    throw new KeyNotFoundException($"Question with ID {request.Id} not found.");

                question.Title = request.Title;
                question.Type = request.Type;
                question.UpdatedAt = DateTime.UtcNow;

                questionRepo.Update(question);


                // var existingChoices = await choiceRepo.GetAll(c => c.QuestionId == question.Id);
           
                var existingChoices = (await choiceRepo.GetAll().ToListAsync())
                                       .Where(c => c.QuestionId == question.Id)
                                       .ToList();



                foreach (var oldChoice in existingChoices)
                {
                    choiceRepo.Delete(oldChoice);
                }

             
                foreach (var newChoiceDto in request.Choices)
                {
                    var newChoice = new Choice
                    {
                        Content = newChoiceDto.Content,
                        IsCorrect = newChoiceDto.IsCorrect,
                        QuestionId = question.Id
                    };
                    await choiceRepo.CreateAsync(newChoice);
                }

                await _unitOfWork.SaveChangesAsync();

               
                return new QuestionDto
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type,
                    Choices = request.Choices
                };
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                throw new ApplicationException($"Error while updating question: {inner}");
            }
        }
    }
}
