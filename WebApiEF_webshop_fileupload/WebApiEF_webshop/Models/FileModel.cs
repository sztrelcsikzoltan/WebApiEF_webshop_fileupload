using Microsoft.AspNetCore.Http;

namespace WebApiEF_webshop.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
