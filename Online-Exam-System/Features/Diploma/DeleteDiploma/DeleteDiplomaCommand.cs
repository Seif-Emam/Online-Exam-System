using MediatR;

namespace Online_Exam_System.Features.Diploma.DeleteDiploma
{
    public record DeleteDiplomaCommand(Guid Id) : IRequest<bool>;
    
    }

