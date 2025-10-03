namespace Online_Exam_System.Contarcts
{
    public interface IDeleteExamCommandOrchestrator
    {
        Task<bool> DeleteExamAsync(Guid id);
    }
}
