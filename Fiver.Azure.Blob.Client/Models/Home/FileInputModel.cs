using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Fiver.Azure.Blob.Client.Models.Home
{
    public class FileInputModel
    {
        public string Folder { get; set; }
        public List<IFormFile> File { get; set; }
    }
}
