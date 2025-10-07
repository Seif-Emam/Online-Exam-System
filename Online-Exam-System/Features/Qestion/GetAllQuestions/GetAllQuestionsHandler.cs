using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Models.Questions;
using Online_Exam_System.Shared;
using Microsoft.EntityFrameworkCore;


namespace Online_Exam_System.Features.Qestion.GetAllQuestions
{
    public class GetAllQuestionsHandler : IRequestHandler<GetAllQuestionsQuery, PagedResult<QuestionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllQuestionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<QuestionDto>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Question>();

            var query = repo
                .GetAll(false)
                .Include(q => q.Exam)
                .Include(q => q.Choices)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(q =>
                    q.Title.Contains(request.SearchTerm) ||
                    q.Id.ToString().Contains(request.SearchTerm));
            }


            if (!string.IsNullOrWhiteSpace(request.ExamName))
            {
                query = query.Where(q => q.Exam.Title == request.ExamName);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var questions = await query
                .OrderBy(q => q.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Type = q.Type,
                    ExamName = q.Exam.Title,
                    Choices = q.Choices.Select(c => new ChoiceDto
                    {
                        Id = c.Id,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<QuestionDto>
            {
                Items = questions,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}