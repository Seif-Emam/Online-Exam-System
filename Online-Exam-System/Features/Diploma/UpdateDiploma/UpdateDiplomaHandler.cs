using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Diploma.UpdateDiploma
{
    public class UpdateDiplomaHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateDiplomaCommand, UpdateDiplomaDTO>
    {
        public async Task<UpdateDiplomaDTO> Handle(UpdateDiplomaCommand request, CancellationToken cancellationToken)
        {
            try {

                var repo = unitOfWork.GetRepository<Models.Diploma>();
                var diploma = await repo.GetByIdAsync(request.Id);
                if (diploma == null)
                    throw new KeyNotFoundException("Diploma with ID {request.Id} not found.");

                diploma.Title = request.Title;
                diploma.Description = request.Description;

                if (!string.IsNullOrEmpty(request.PictureUrl))
                {
                    diploma.PictureUrl = request.PictureUrl;
                }

                repo.Update(diploma);
                await unitOfWork.SaveChangesAsync();

                return new UpdateDiplomaDTO
                {
                    Id = diploma.Id,
                    Title = diploma.Title,
                    Description = diploma.Description,
                    PictureUrl = diploma.PictureUrl
                };

            } catch (Exception ex) {

                throw new Exception($"Failed to update diploma: {ex.Message}", ex);
            }

        }
    }
}
