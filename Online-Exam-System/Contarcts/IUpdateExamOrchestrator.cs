using Online_Exam_System.Features.Exam.UpdateExam;

namespace Online_Exam_System.Contarcts
{
    public interface IUpdateExamOrchestrator
    {
        public Task<UpdateExamDTO> UpdateExamAsync(Guid id, UpdateExamRequest request);
    }
}
