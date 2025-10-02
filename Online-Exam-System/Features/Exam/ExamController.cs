using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Features.Exam.GetById;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ExamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ExamQueryParameters parameters)
        {
            var query = new GetAllExamQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{Id}")]

        public async Task<ActionResult> GetExamById(Guid Id)
        {
            var Quere = new GetExamByIdQuerey(Id);
            var data = await _mediator.Send(Quere);
            return Ok(data);
        }

    }
}
