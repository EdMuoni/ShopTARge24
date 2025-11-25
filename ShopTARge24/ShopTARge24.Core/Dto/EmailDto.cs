using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.Core.Dto
{
    public class EmailDto
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<IFormFile> Attachment { get; set; } = new List<IFormFile>();

    }
}
