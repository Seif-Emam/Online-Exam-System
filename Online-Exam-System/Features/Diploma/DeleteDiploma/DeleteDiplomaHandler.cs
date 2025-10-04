using MediatR;
using Online_Exam_System.Contarcts;

namespace Online_Exam_System.Features.Diploma.DeleteDiploma
{
    public class DeleteDiplomaHandler : IRequestHandler<DeleteDiplomaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDiplomaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDiplomaCommand request, CancellationToken cancellationToken)
        {
 
            var repo = _unitOfWork.GetRepository<Models.Diploma>();
            var diploma = await repo.GetByIdAsync(request.Id);
            if (diploma == null)
                throw new KeyNotFoundException("Diploma not found.");

            diploma.IsDeleted = true;
            repo.Update(diploma);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
    
