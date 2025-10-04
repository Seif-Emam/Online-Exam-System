using MediatR;

namespace Online_Exam_System.Features.Diploma.UpdateDiploma
{
    public record UpdateDiplomaCommand(Guid Id, string Title, string Description, string? PictureUrl)
           : IRequest<UpdateDiplomaDTO>; 
    }

