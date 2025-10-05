using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models;

namespace Online_Exam_System.Features.Exam.AddExam
{
    public class AddExamHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddExamCommend, AddExamDTO>
    {
        public async Task<AddExamDTO> Handle(AddExamCommend request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Step 1: Check if the related Diploma exists
                var diplomaRepo = unitOfWork.GetRepository<Models.Diploma>();
                var diploma = await diplomaRepo.GetByIdAsync(request.DiplomaId);

                if (diploma is null)
                    throw new KeyNotFoundException($"Diploma with ID {request.DiplomaId} not found.");

                // ✅ Step 2: Create a new Exam entity and assign properties
                var examRepo = unitOfWork.GetRepository<Models.Exam>();
                var exam = new Models.Exam
                {
                    Title = request.Title,
                    Duration = request.Duration,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    PictureUrl = request.PictureUrl,
                    DiplomaId = request.DiplomaId
                };

                // ✅ Step 3: Add the exam to the repository and save changes
                await examRepo.CreateAsync(exam);
                await unitOfWork.SaveChangesAsync();

                // ✅ Step 4: Map the entity to a DTO for the response
                return new AddExamDTO
                {
                    Id = exam.Id,
                    Title = exam.Title,
                    PictureUrl = exam.PictureUrl,
                    StartDate = exam.StartDate,
                    EndDate = exam.EndDate,
                    Duration = exam.Duration
                };
            }
            catch (KeyNotFoundException)
            {
                // ⚠️ Throw again to be handled by global middleware (returns 404)
                throw;
            }
            catch (Exception ex)
            {
                // 💥 Unexpected error: handled by global middleware (returns 500)
                throw new ApplicationException("An error occurred while adding the exam.", ex);
            }
        }
    }
}
