using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        private IWebHostEnvironment Environment;

        public UploadFileController(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        [Route("UploadFiles")]
        [HttpPost]
        public IActionResult UploadFiles()
        {
            //Create the Directory.
            string path = Path.Combine(this.Environment.WebRootPath, "Uploads\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            IFormFile postedFile = Request.Form.Files[0];

            //Fetch the File Name.
            string fileName = Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

            //Save the File.
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            //Send OK Response to Client.
            return Ok(fileName);
        }
    }
}
