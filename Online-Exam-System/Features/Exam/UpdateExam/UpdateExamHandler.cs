using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Exam.UpdateExam
{
    public class UpdateExamHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateExamCommand, UpdateExamDTO>
    {
        public async  Task<UpdateExamDTO> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
           
                if (string.IsNullOrWhiteSpace(request.Title))
                    throw new ArgumentException("Title is required and cannot be empty.");

                if (string.IsNullOrEmpty(request.PictureUrl))
                    throw new ArgumentException("Image is required.");

               
                if (request.StartDate < DateOnly.FromDateTime(DateTime.Now))
                    throw new ArgumentException("Start date must be after the current date.");

                if (request.EndDate < request.StartDate)
                    throw new ArgumentException("End date must be equal to or later than start date.");

               
                var minDuration = new TimeOnly(0, 20, 0);
                var maxDuration = new TimeOnly(3, 0, 0);

                if (request.Duration < minDuration || request.Duration > maxDuration)
                    throw new ArgumentException("Duration must be between 20 minutes and 3 hours.");

                var repo = unitOfWork.GetRepository<Models.Exam>();
                var exam = await repo.GetByIdAsync(Guid.Parse(request.id.ToString()));

                if (exam == null)
                    throw new KeyNotFoundException("Exam not found.");

             
                exam.Title = request.Title;
                exam.PictureUrl = request.PictureUrl;
                exam.StartDate = request.StartDate;
                exam.EndDate = request.EndDate;
                exam.Duration = request.Duration;

                
          
                repo.Update(exam);
                await unitOfWork.SaveChangesAsync();

              
                return new UpdateExamDTO
                {
                    Id = exam.Id,
                    Title = exam.Title,
                    PictureUrl = exam.PictureUrl,
                    StartDate = exam.StartDate,
                    EndDate = exam.EndDate,
                    Duration = exam.Duration
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update exam: {ex.Message}", ex);
            }
        }
    }
}
