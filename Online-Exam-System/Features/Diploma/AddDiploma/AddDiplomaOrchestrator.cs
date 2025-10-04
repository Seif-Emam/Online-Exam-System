using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Services;

namespace Online_Exam_System.Features.Diploma.AddDiploma
{
    public class AddDiplomaOrchestrator : IAddDiplomaOrchestrator
    {
        private readonly IImageHelper _imageHelper;
        private readonly IMediator _mediator;
        public AddDiplomaOrchestrator(IImageHelper imageHelper, IMediator mediator)
        {
            _mediator = mediator;
            _imageHelper = imageHelper;
        }
        public async Task<AddDiplomaDTO> AddDiplomaAsync(AddDiplomaRequest request)
        {
            try
            {
                if (request.PictureUrl == null)
                    throw new ArgumentException("Picture file is required");
                var pictureUrl= await _imageHelper.SaveImageAsync(request.PictureUrl, "Diplomas");
                var command = new AddDiplomaCommend(
                    request.Title,
                    request.Description,
                    pictureUrl
                    );
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while processing AddDiploma: {ex.Message}", ex);
            }
        }
    }
}