using Microsoft.AspNetCore.Http;

namespace Fiver.Azure.Blob.Client.Models.Home
{
    public class FileInputModel
    {
        public string Folder { get; set; }
        public IFormFile File { get; set; }
    }
}
