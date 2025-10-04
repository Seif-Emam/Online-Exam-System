using MediatR;

namespace Online_Exam_System.Features.Diploma.AddDiploma
{
    public record AddDiplomaCommend(string Title ,string Description , string PictureUrl ) :IRequest<AddDiplomaDTO>
    {
    }
}
