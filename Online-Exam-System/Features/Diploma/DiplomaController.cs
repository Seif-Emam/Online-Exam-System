using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Diploma.AddDiploma;
using Online_Exam_System.Features.Diploma.DeleteDiploma;
using Online_Exam_System.Features.Diploma.GetAllDiplomas;
using Online_Exam_System.Features.Diploma.GetDiplomaById;
using Online_Exam_System.Features.Diploma.UpdateDiploma;
using Online_Exam_System.Features.Exam.AddExam;
using Online_Exam_System.Shared;

namespace Online_Exam_System.Features.Diploma
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiplomaController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IAddDiplomaOrchestrator _addDiplomaOrchestrator;
        private readonly IUpdateDiplomaOrchestrator _updateDiplomaOrchestrator;
        private readonly IDeleteDiplomaOrchestrator _deleteDiplomaOrchestrator;
        public DiplomaController(IMediator mediator,IDeleteDiplomaOrchestrator deleteDiplomaOrchestrator ,IAddDiplomaOrchestrator addDiplomaOrchestrator, IUpdateDiplomaOrchestrator updateDiplomaOrchestrator)
        {

            _mediator = mediator;
            _addDiplomaOrchestrator = addDiplomaOrchestrator;
            _updateDiplomaOrchestrator = updateDiplomaOrchestrator;
            _deleteDiplomaOrchestrator = deleteDiplomaOrchestrator;
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



        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<AddDiplomaDTO>> AddDiploma([FromForm] AddDiplomaRequest request)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to add diplomas.");

            var result = await _addDiplomaOrchestrator.AddDiplomaAsync(request);
            return Ok(new
            {
                message = "Diploma added successfully",
                data = result
            });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> UpdateDiploma(Guid id, [FromForm] UpdateDiplomaRequest request)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to update diplomas.");

            var result = await _updateDiplomaOrchestrator.UpdateDiplomaAsync(id, request);
            return Ok(new
            {
                message = "Diploma updated successfully",
                data = result
            });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteDiploma(Guid id)
        {
            if (!User.IsInRole("Admin"))
                throw new UnauthorizedAccessException("You do not have permission to delete diplomas.");

            var result = await _deleteDiplomaOrchestrator.DeleteDiplomaAsync(id);
            return Ok(new
            {
                message = "Diploma deleted successfully (soft delete).",
                success = result
            });
        }

    }
}


