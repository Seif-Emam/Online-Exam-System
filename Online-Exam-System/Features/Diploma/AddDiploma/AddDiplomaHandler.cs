using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Diploma.AddDiploma
{
    public class AddDiplomaHandler: IRequestHandler<AddDiplomaCommend, AddDiplomaDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddDiplomaHandler(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;

        }

        public async Task<AddDiplomaDTO> Handle(AddDiplomaCommend request, CancellationToken cancellationToken)
        {

            try {
                if(string.IsNullOrWhiteSpace(request.Title))
                    throw new ArgumentException("Diploma Title is required and cannot be empty.");

                var repo = _unitOfWork.GetRepository<Models.Diploma>();
                var diploma = new Models.Diploma
                {
                    Title = request.Title,
                    Description = request.Description,
                    PictureUrl = request.PictureUrl
                };

                await repo.CreateAsync(diploma);
                await _unitOfWork.SaveChangesAsync();
                return new AddDiplomaDTO
                {
                    Id = diploma.Id,
                    Title = diploma.Title,
                    Description = diploma.Description,
                    PictureUrl = diploma.PictureUrl
                };

            } catch (Exception ex) { 
            
                throw new Exception("Error adding diploma", ex);


            }
        }
    }
}