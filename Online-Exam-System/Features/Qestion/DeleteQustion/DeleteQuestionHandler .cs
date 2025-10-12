using MediatR;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models.Questions;

namespace Online_Exam_System.Features.Qestion.DeleteQustion
{
    public class DeleteQuestionHandler : IRequestHandler<DeleteQuestionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questionRepo = _unitOfWork.GetRepository<Question>();
                var choiceRepo = _unitOfWork.GetRepository<Choice>();

                var question = await questionRepo.GetByIdAsync(request.Id);
                if (question == null)
                    throw new ApplicationException($"Question with ID {request.Id} not found.");

             
      
                var choices = (await choiceRepo.GetAll().ToListAsync())
                                      .Where(c => c.QuestionId == question.Id)
                                      .ToList();
                foreach (var choice in choices)
                {
                    choiceRepo.Delete(choice);
                }


                questionRepo.Delete(question);

 
                return await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new ApplicationException($"Error while deleting question: {innerMessage}");
            }
        }
    }
}
