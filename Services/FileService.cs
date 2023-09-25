using CashierApi.Helpers;

namespace CashierApi.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;   
        }
        public async Task<Tuple<int, string>> SaveImageAsync(IFormFile imageFile,string folderName)
        {
            try
            {
                var contentPath = _webHostEnvironment.WebRootPath;  
                var path = Path.Combine(contentPath, "uploads\\"+folderName );
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // Check the allowed extensions
                var ext = Path.GetExtension(imageFile.FileName);

                if(!ImageFilesExtensionsValidator.IsImageExtension(ext))
                {
                    string message = "Only images extensions are allowed!";
                    return new Tuple<int, string>(0,message);
                }
                string uniqueString = Path.GetRandomFileName();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path,newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);

            }
            catch(Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        public bool DeleteImage(string imageFileName, string folderName) { 
            try
            {
                var wwwPath = _webHostEnvironment.WebRootPath;
                var path = Path.Combine(wwwPath, "uploads\\" + folderName, imageFileName);
                if(File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                return false;

            }
        }

    
    }
}
