using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebApiEF_webshop.Models;

namespace WebApiEF_webshop.Models
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpPost]
        public ActionResult Post([FromForm] FileModel file)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", file.FileName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    if (file.FormFile != null)
                    {
                        file.FormFile.CopyTo(stream);
                    }
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
