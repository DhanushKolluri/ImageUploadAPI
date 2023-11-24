using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public class FileUploadAPI
        {
            public IFormFile files
            { 
                get;
                set;
            }
        }
        [HttpPost]
        public async Task<string> Post([FromBody] FileUploadAPI fileUploadAPI)
        {
            if (fileUploadAPI.files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\"+ fileUploadAPI.files.FileName))
                    {
                        fileUploadAPI.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\uploads\\" + fileUploadAPI.files.FileName;

                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Unsuccessful";
            }
        }
    }
}
