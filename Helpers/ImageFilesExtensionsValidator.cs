using System.ComponentModel.DataAnnotations;

namespace CashierApi.Helpers
{
    public class ImageFilesExtensionsValidator
    {
       public static bool IsImageExtension(string extension)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            return Array.IndexOf(allowedExtensions, extension.ToLower()) >= 0;
        }
    }
}
