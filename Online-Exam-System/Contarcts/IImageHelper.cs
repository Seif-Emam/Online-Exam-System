﻿namespace Online_Exam_System.Contarcts
{
    public interface IImageHelper
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string subFolder);

        string GetImageUrl(string relativePath);

        bool DeleteImage(string relativePath);
    }
}
