using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Qestion.CreateQuestion;
using Online_Exam_System.Features.Qestion.DeleteQustion;
using Online_Exam_System.Features.Qestion.GetAllQuestions;
using Online_Exam_System.Features.Qestion.GetQuestionById;
using Online_Exam_System.Features.Qestion.GetQuestionTypes;
using Online_Exam_System.Features.Qestion.UpdateQustions;
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

        [HttpGet("GetAll")]
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

        [HttpPost("Create")]
        public async Task<ActionResult<QuestionDto>> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}
