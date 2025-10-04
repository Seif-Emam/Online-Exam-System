using Online_Exam_System.Features.Diploma.UpdateDiploma;

namespace Online_Exam_System.Contarcts
{
    public interface IUpdateDiplomaOrchestrator
    {
        Task<UpdateDiplomaDTO> UpdateDiplomaAsync(Guid id , UpdateDiplomaRequest request);
    }
}
