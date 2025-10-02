using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Online_Exam_System.Features.Exam.GetById
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetExamByIdEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetExamByIdEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult> GetExamById(Guid Id)
        {
            var Quere = new GetExamByIdQuerey(Id);
            var data =await _mediator.Send(Quere);
            return Ok(data);
        }

    }
}
