using MediatR;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam.GetAll
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllExamEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllExamEndPoint(IMediator mediator)
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
    }
}
