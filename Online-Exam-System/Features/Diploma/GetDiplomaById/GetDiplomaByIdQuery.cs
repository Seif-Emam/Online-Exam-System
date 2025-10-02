using MediatR;

namespace Online_Exam_System.Features.Diploma.GetDiplomaById
{
    public record GetDiplomaByIdQuery (Guid Id) : IRequest<GetDiplomaByIdDTO>
    {
    }
}
