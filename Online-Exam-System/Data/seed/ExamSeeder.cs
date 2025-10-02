using Online_Exam_System.Models;
using System.Text.Json;

namespace Online_Exam_System.Data.Seed
{
    public static class ExamSeeder
    {
        public static async Task SeedExamsAsync(OnlineExamContext context)
        {
            if (context.Exams.Any())
                return;

            try
            {
                // ✅ Load JSON data from file
                var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "exam.json");

                if (!File.Exists(jsonPath))
                    throw new FileNotFoundException($"Exam seed file not found at path: {jsonPath}");

                var jsonData = await File.ReadAllTextAsync(jsonPath);
                var exams = JsonSerializer.Deserialize<List<Exam>>(jsonData);

                if (exams == null || !exams.Any())
                    throw new InvalidOperationException("No valid exam data found in seed file.");

                await context.Exams.AddRangeAsync(exams);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Error while seeding Exams: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }
    }
}