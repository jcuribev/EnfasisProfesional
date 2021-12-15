using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorias.Models
{
    public class ImageItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile ImageFile {get;set;}
    }
}
