using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Qestion
{
    [Route("api/[controller]")]
    [ApiController]
    public class QestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult<PagedResult<QuestionDto>>> GetAllQuestions([FromForm] GetAllQuestionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
