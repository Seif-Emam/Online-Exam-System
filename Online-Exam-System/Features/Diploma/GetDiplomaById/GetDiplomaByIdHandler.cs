using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Diploma.GetDiplomaById
{
    public class GetDiplomaByIdHandler : IRequestHandler<GetDiplomaByIdQuery, GetDiplomaByIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public GetDiplomaByIdHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<GetDiplomaByIdDTO> Handle(GetDiplomaByIdQuery request, CancellationToken cancellationToken)
        {
            // Cache key for this diploma
            string cacheKey = $"Diploma_{request.Id}";

            // Try to get from cache first
            if (!_cache.TryGetValue(cacheKey, out Models.Diploma diploma))
            {
                // Fetch from database
                diploma = await _unitOfWork
                    .GetRepository<Models.Diploma>()
                    .GetByIdAsync(request.Id);

                // If not found → throw 404
                if (diploma == null)
                    throw new KeyNotFoundException($"Diploma with Id {request.Id} not found.");

                // Cache the diploma for 5 minutes
                _cache.Set(cacheKey, diploma, TimeSpan.FromMinutes(5));
            }

            // Return DTO
            return new GetDiplomaByIdDTO
            {
                Id = diploma.Id,
                Title = diploma.Title,
                Description = diploma.Description,
                PictureUrl = diploma.PictureUrl,
                CreatedAt = diploma.CreatedAt
            };
        }
    }
}
