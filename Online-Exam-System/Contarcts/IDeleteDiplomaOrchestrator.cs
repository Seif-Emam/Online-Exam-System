namespace Online_Exam_System.Contarcts
{
    public interface IDeleteDiplomaOrchestrator
    {
        Task<bool> DeleteDiplomaAsync(Guid id);

    }
}
