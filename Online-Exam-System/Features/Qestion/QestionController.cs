using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Features.Qestion.GetQuestionById;
using Online_Exam_System.Features.Qestion.GetQuestionTypes;
using Online_Exam_System.Shared;
using QuestionDto = Online_Exam_System.Features.Qestion.GetAllQuestions.QuestionDto;

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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(Guid id)
        {
            var query = new GetQuestionByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<QuestionTypeDto>>> GetQuestionTypes()
        {
            var result = await _mediator.Send(new GetQuestionTypesQuery());
            return Ok(result);
        }

    }
}
