using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam.GetAll
{
    public class GetAllExamHandler : IRequestHandler<GetAllExamQuery, PagedResult<GetAllExamsDTOs>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetAllExamHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<PagedResult<GetAllExamsDTOs>> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Models.Exam> exams;

                // ✅ Always store detached data (ToList)
                if (!_cache.TryGetValue("AllExams", out exams))
                {
                    exams = _unitOfWork.GetRepository<Models.Exam>()
                        .GetAll()
                        .ToList(); // Detach from DbContext

                    if (exams == null || !exams.Any())
                        throw new KeyNotFoundException("No exams found.");

                    _cache.Set("AllExams", exams, TimeSpan.FromMinutes(5));
                }

                var query = exams.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
                {
                    var search = request.Parameters.Search.Trim();
                    query = query.Where(e =>
                        e.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        e.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                    );
                }

                if (request.Parameters.StartDate.HasValue)
                    query = query.Where(e => e.StartDate >= request.Parameters.StartDate.Value);

                if (request.Parameters.EndDate.HasValue)
                    query = query.Where(e => e.EndDate <= request.Parameters.EndDate.Value);

                if (request.Parameters.Duration.HasValue)
                    query = query.Where(e => e.Duration.ToTimeSpan().TotalMinutes == request.Parameters.Duration.Value);

                var totalCount = query.Count();

                int pageSize = request.Parameters.PageSize <= 0 ? 10 : request.Parameters.PageSize;
                int pageNumber = request.Parameters.PageNumber <= 0 ? 1 : request.Parameters.PageNumber;

                var examsPage = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(e => new GetAllExamsDTOs
                    {
                        Id = e.Id,
                        Title = e.Title,
                        PictureUrl = e.PictureUrl,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Duration = e.Duration
                    })
                    .ToList();

                return new PagedResult<GetAllExamsDTOs>
                {
                    Items = examsPage,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the list of exams.", ex);
            }
        }
    }
}
