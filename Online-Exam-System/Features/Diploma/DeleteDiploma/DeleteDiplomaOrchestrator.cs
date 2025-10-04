using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Diploma.DeleteDiploma
{
    public class DeleteDiplomaOrchestrator : IDeleteDiplomaOrchestrator
    {
        private readonly IImageHelper _imageHelper;
        private readonly IMediator _mediator;
        public DeleteDiplomaOrchestrator(IImageHelper imageHelper, IMediator mediator)
        {
            _imageHelper = imageHelper;
            _mediator = mediator;
        }

        public async Task<bool> DeleteDiplomaAsync(Guid id)
        {
            try
            {
                return await _mediator.Send(new DeleteDiplomaCommand(id));
            }
            catch (KeyNotFoundException )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the diploma.", ex);
            }
        }
    }

}