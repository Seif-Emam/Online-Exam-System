using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Exam.AddExam;
using Online_Exam_System.Features.Exam.GetAll;
using Online_Exam_System.Features.Exam.GetByDiploma;
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

        public ExamController(
            IMediator mediator,
            IAddExamOrchestrator addExamOrchestrator,
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetExamById(Guid id)
        {
            var query = new GetExamByIdQuerey(id);
            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet("ByDiploma/{diplomaId:guid}")]
        public async Task<ActionResult> GetExamsByDiploma(Guid diplomaId)
        {
            var query = new GetExamsByDiplomaQuery(diplomaId);
            var result = await _mediator.Send(query);

            return Ok(new
            {
                success = true,
                message = "Exams retrieved successfully.",
                data = result
            });
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<AddExamDTO>> AddExam([FromForm] AddExamRequest request)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");

            var exam = await _addExamOrchestrator.AddExamAsync(request);
            return Ok(exam);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<UpdateExamDTO>> UpdateExam(Guid id, [FromForm] UpdateExamRequest request)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");

            var exam = await _updateExamOrchestrator.UpdateExamAsync(id, request);
            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteExam(Guid id)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");

            var result = await _deleteExamOrchestrator.DeleteExamAsync(id);

            return Ok(new
            {
                message = "Exam deleted successfully (soft delete).",
                success = result
            });
        }
    }
}
