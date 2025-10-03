using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Exam.GetById
{
    public class GetExamByIdHandler : IRequestHandler<GetExamByIdQuerey, GetExamsByIdDTOs>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExamByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetExamsByIdDTOs> Handle(GetExamByIdQuerey request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch exam by ID
                var exam = await _unitOfWork
                    .GetRepository<Models.Exam>()
                    .GetByIdAsync(request.Id);

                // If not found → throw 404
                if (exam == null)
                    throw new KeyNotFoundException($"Exam with Id {request.Id} not found.");

                // Return DTO
                return new GetExamsByIdDTOs
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
                // Let middleware handle 404
                throw;
            }
            catch (Exception ex)
            {
                // General error → handled by GlobalExceptionMiddleware
                throw new ApplicationException($"An error occurred while fetching the exam with Id {request.Id}.", ex);
            }
        }
    }
}
