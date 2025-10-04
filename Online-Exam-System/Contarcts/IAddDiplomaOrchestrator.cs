using Online_Exam_System.Features.Diploma.AddDiploma;
using Online_Exam_System.Features.Exam.AddExam;

namespace Online_Exam_System.Contarcts
{
    public interface IAddDiplomaOrchestrator
    {
        Task<AddDiplomaDTO> AddDiplomaAsync(AddDiplomaRequest request);

    }
}
