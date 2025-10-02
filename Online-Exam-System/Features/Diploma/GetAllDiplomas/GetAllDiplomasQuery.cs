using MediatR;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Diploma.GetAllDiplomas
{
    public record GetAllDiplomasQuery(DiplomaQueryParameters Parameters) : IRequest<PagedResult<GetAllDiplomasDTO>>
    {
    }
}
