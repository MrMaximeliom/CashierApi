namespace CashierApi.Services
{
    public interface IFileService
    {
        public Task<Tuple<int, string>> SaveImageAsync(IFormFile imageFile,string folderName);

        public bool DeleteImage(string imageFileName, string folderName);


    }
}
