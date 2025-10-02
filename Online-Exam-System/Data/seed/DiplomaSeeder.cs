using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using System.Text.Json;

namespace Online_Exam_System.Data.Seed
{
    public static class DiplomaSeeder
    {
        public static async Task SeedDiplomasAsync(OnlineExamContext context)
        {
            // ✅ لو الجدول فيه بيانات، ما نكررش
            if (await context.Doplomas.AnyAsync())
                return;

            // 📂 المسار الكامل لملف JSON
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "diplomas.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Diplomas seed file not found.", filePath);

            // 📖 قراءة الملف
            var jsonData = await File.ReadAllTextAsync(filePath);

            // 🔄 تحويل JSON إلى List<Diploma>
            var diplomas = JsonSerializer.Deserialize<List<Diploma>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (diplomas == null || !diplomas.Any())
                throw new Exception("Diploma seed file is empty or invalid.");

            // 🔑 تجهيز بيانات إضافية
            foreach (var diploma in diplomas)
            {
                diploma.Id = Guid.NewGuid();
                diploma.CreatedAt = DateTime.UtcNow;
                diploma.UpdatedAt = DateTime.UtcNow;
                diploma.IsDeleted = false;
            }

            // 💾 إضافة البيانات
            await context.Doplomas.AddRangeAsync(diplomas);
            await context.SaveChangesAsync();
        }
    }
}
