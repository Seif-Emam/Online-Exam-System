using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.AddExam;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Features.Exam.GetById;
using Online_Exam_System.Features.Exam.UpdateExam;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Exam
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IAddExamOrchestrator _addExamOrchestrator;
        private readonly IUpdateExamOrchestrator _updateExamOrchestrator;
        private readonly IDeleteExamCommandOrchestrator _deleteExamOrchestrator;

        public ExamController(IMediator mediator,
            IAddExamOrchestrator addExamOrchestrator ,
            IUpdateExamOrchestrator updateExamOrchestrator,
            IDeleteExamCommandOrchestrator deleteExamOrchestrator)
        {
            _mediator = mediator;
            _addExamOrchestrator = addExamOrchestrator;
            _updateExamOrchestrator = updateExamOrchestrator;
            _deleteExamOrchestrator = deleteExamOrchestrator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ExamQueryParameters parameters)
        {
            var query = new GetAllExamQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult> GetExamById(Guid id)
        {
            var Quere = new GetExamByIdQuerey(id);
            var data = await _mediator.Send(Quere);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<AddExamDTO>> AddExam([FromForm]AddExamRequest request)
        {
            var Exam = await _addExamOrchestrator.AddExamAsync(request);
            return Ok(Exam);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateExamDTO>> UpdateExam (Guid id , [FromForm] UpdateExamRequest request)
        {
            var Exam = await _updateExamOrchestrator.UpdateExamAsync(id, request);
            if (Exam == null)
                return NotFound();
            return Ok(Exam);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExam(Guid id)
        {
            var Exam =await _deleteExamOrchestrator.DeleteExamAsync(id);
            if (!Exam)
                return NotFound();
            return NoContent();
        }
    }
}
