using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Shared;
using System.Collections.Generic;

namespace Online_Exam_System.Features.Diploma.GetAllDiplomas
{
    public class GetAllDiplomasHandler(IUnitOfWork _unitOfWork, IMemoryCache _cache) :
        IRequestHandler<GetAllDiplomasQuery, PagedResult<GetAllDiplomasDTO>>
    {
        public async Task<PagedResult<GetAllDiplomasDTO>> Handle(GetAllDiplomasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!_cache.TryGetValue("AllDiplomas", out IEnumerable<Models.Diploma> diplomas))
                {
                    diplomas =  _unitOfWork.GetRepository<Models.Diploma>().GetAll();
                    _cache.Set("AllDiplomas", diplomas, TimeSpan.FromMinutes(5));
                }

                var query = diplomas;

                if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
                {
                    var search = request.Parameters.Search.Trim();
                    query = query.Where(d =>
                        d.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (d.Description != null && d.Description.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        d.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                    );
                }

                int pageSize = Math.Clamp(request.Parameters.PageSize, 1, 100);
                int pageNumber = request.Parameters.PageNumber <= 0 ? 1 : request.Parameters.PageNumber;

                var totalCount = query.Count();

                var diplomasPage = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(d => new GetAllDiplomasDTO
                    {
                        Id = d.Id,
                        Title = d.Title,
                        Description = d.Description,
                        PictureUrl = d.PictureUrl
                    })
                    .ToList();

                return new PagedResult<GetAllDiplomasDTO>
                {
                    Items = diplomasPage,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching diplomas.", ex);
            }
        }

    }
}
