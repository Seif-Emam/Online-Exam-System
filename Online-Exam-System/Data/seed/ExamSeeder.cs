using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using System.Text.Json;

namespace Online_Exam_System.Data.Seed
{
    public static class ExamSeeder
    {
        public static async Task SeedExamsAsync(OnlineExamContext context)
        {
            // ✅ لو فيه امتحانات قبل كده، ما تكررش
            if (await context.Exams.AnyAsync())
                return;

            // ✅ تأكد إن فيه دبلومات متسجلة
            var diplomas = await context.Diplomas.ToListAsync();
            if (!diplomas.Any())
                throw new Exception("❌ No diplomas found. Please seed diplomas first.");

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

                // 🧠 اربط كل امتحان بدبلومة موجودة فعلاً
                var random = new Random();
                foreach (var exam in exams)
                {
                    exam.Id = Guid.NewGuid(); // يولد ID جديد للامتحان
                    exam.DiplomaId = diplomas[random.Next(diplomas.Count)].Id; // ✅ يربطه بدبلومة حقيقية
                    exam.CreatedAt = DateTime.UtcNow;
                    exam.UpdatedAt = DateTime.UtcNow;
                    exam.IsDeleted = false;
                }

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
