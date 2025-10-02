using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam.GetAll
{
    public class GetAllExamHandler(IUnitOfWork _unitOfWork, IMemoryCache _cache) : IRequestHandler<GetAllExamQuery, PagedResult<GetAllExamsDTOs>>
    {

        public async Task<PagedResult<GetAllExamsDTOs>> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
           //cash
            if (!_cache.TryGetValue("AllExams", out IEnumerable<Models.Exam> exams))
            {
                exams = await _unitOfWork.GetRepository<Models.Exam>().GetAllAsync();
                _cache.Set("AllExams", exams, TimeSpan.FromMinutes(5));
            }

            var query = exams;

            //  Search
            if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            {
                var search = request.Parameters.Search.Trim();
                query = query.Where(e =>
                    e.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    e.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Filter
            if (request.Parameters.StartDate.HasValue)
                query = query.Where(e => e.StartDate >= request.Parameters.StartDate.Value);

            //  Filter 
            if (request.Parameters.EndDate.HasValue)
                query = query.Where(e => e.EndDate <= request.Parameters.EndDate.Value);

            // 5. Filter Duration 
            if (request.Parameters.Duration.HasValue)
                query = query.Where(e => e.duration.ToTimeSpan().TotalMinutes == request.Parameters.Duration.Value);

            
            var totalCount = query.Count();

            // 7. Pagination
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
                    Duration = e.duration
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
    }
}

