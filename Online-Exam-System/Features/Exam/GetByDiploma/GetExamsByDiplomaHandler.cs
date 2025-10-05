using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.GetAll;

namespace Online_Exam_System.Features.Exam.GetByDiploma
{
    public class GetExamsByDiplomaHandler : IRequestHandler<GetExamsByDiplomaQuery, IEnumerable<GetAllExamsDTOs>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetExamsByDiplomaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetAllExamsDTOs>> Handle(GetExamsByDiplomaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Step 1: Get the repository for Exam
                var repo = _unitOfWork.GetRepository<Models.Exam>();

                // ✅ Step 2: Filter exams by DiplomaId
                var examsQuery = repo.FindByCondition(e => e.DiplomaId == request.DiplomaId);

                // ✅ Step 3: Check if no exams were found
                if (examsQuery is null || !examsQuery.Any())
                    throw new KeyNotFoundException($"No exams found for Diploma ID: {request.DiplomaId}");

                // ✅ Step 4: Project entities into DTOs
                var exams = examsQuery.Select(e => new GetAllExamsDTOs
                {
                    Id = e.Id,
                    Title = e.Title,
                    PictureUrl = e.PictureUrl,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Duration = e.Duration
                }).ToList();

                return exams;
            }
            catch (KeyNotFoundException)
            {
                // ⚠️ Not found — rethrow to be handled by global exception middleware (404)
                throw;
            }
            catch (Exception ex)
            {
                // 💥 Unexpected error — handled by global exception middleware (500)
                throw new ApplicationException("An error occurred while retrieving exams by diploma.", ex);
            }
        }
    }
}
