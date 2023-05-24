using Microsoft.VisualBasic.FileIO;
using NuGet.ContentModel;

namespace ASPNETCoreApplication.Entities
{
    public class FileUploadModel
    {
        public IFormFile?  FileDetails { get; set; }
        // public FileType FileType { get; set; }
       
        

        public FileDetails? FileDetail { get; set; }
        public IFormFile? file { get; set; }
        public int assetid { get; set; }
    }
}
