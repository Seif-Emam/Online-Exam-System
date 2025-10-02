using MediatR;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam.GetAll
{
    public record GetAllExamQuery(ExamQueryParameters Parameters) : IRequest<PagedResult<GetAllExamsDTOs>>;
       
}
