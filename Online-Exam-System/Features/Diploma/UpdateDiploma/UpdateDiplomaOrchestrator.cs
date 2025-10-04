using MediatR;
using Online_Exam_System.Contarcts;
using Online_Exam_System.Features.Diploma.GetDiplomaById;
using Online_Exam_System.Services;

namespace Online_Exam_System.Features.Diploma.UpdateDiploma
{
    public class UpdateDiplomaOrchestrator : IUpdateDiplomaOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly IImageHelper _imageHelper;

        public UpdateDiplomaOrchestrator(IMediator mediator, IImageHelper imageHelper)
        {
            _mediator = mediator;
            _imageHelper = imageHelper;
        }

        public async Task<UpdateDiplomaDTO> UpdateDiplomaAsync(Guid id, UpdateDiplomaRequest request)
        {
            // 🧩 1️⃣ Get existing diploma (not updating the Id)
            var diploma = await _mediator.Send(new GetDiplomaByIdQuery(id));
            if (diploma == null)
                throw new KeyNotFoundException("Diploma not found.");

            string? pictureUrl = diploma.PictureUrl;

            // 🖼️ 2️⃣ Handle image update safely
            try
            {
                if (request.PictureUrl is not null && request.PictureUrl.Length > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(diploma.PictureUrl))
                        _imageHelper.DeleteImage(diploma.PictureUrl);

                    // Save new image
                    pictureUrl = await _imageHelper.SaveImageAsync(request.PictureUrl, "Diplomas");
                }
            }
            catch (Exception ex)
            {
                // Let the middleware handle it
                throw new Exception("Error occurred while processing the diploma image.", ex);
            }

            // 🧱 3️⃣ Send update command (note: we use the same Id, not updating it)
            var updatedDiploma = await _mediator.Send(new UpdateDiplomaCommand(
                diploma.Id, // ← existing ID
                request.Title,
                request.Description,
                pictureUrl
            ));

            return updatedDiploma;
        }
    }
}
