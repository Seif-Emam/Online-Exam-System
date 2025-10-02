using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Features.Diploma.GetAllDiplomas;
using Online_Exam_System.Features.Diploma.GetDiplomaById;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Diploma
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiplomaController : ControllerBase
    {

        private readonly IMediator _mediator;
        public DiplomaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] DiplomaQueryParameters parameters)
        {

            var query = new GetAllDiplomasQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetDiplomaByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}


