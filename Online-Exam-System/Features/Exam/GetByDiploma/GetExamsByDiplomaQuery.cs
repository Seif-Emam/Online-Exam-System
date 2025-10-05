using MediatR;
using Online_Exam_System.Features.Exam.GetAll;

namespace Online_Exam_System.Features.Exam.GetByDiploma
{
    public record GetExamsByDiplomaQuery(Guid DiplomaId) : IRequest<IEnumerable<GetAllExamsDTOs>>;

    }

