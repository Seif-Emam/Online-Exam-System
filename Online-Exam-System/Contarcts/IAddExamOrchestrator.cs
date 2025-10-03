using Online_Exam_System.Features.Exam.AddExam;

namespace Online_Exam_System.Contarcts
{
    public interface IAddExamOrchestrator
    {
        Task<AddExamDTO> AddExamAsync(AddExamRequest request);
    }
}
