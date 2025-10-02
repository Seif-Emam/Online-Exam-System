using MediatR;

namespace Online_Exam_System.Features.Exam.GetById
{
    public record GetExamByIdQuerey(Guid Id) : IRequest<GetExamsByIdDTOs>;
   
}
