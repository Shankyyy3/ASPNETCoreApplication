using ASPNETCoreApplication.Entities;

namespace ASPNETCoreApplication.Services
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, int assetId);
        //, FileType fileType

        public Task PostMultiFileAsync(List<FileUploadModel> fileData);

        public Task DownloadFileById(int fileName);
    }
}
