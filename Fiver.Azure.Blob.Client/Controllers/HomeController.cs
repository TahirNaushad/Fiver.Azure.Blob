using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Fiver.Azure.Blob.Client.Models.Home;

namespace Fiver.Azure.Blob.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAzureBlobStorage blobStorage;

        public HomeController(IAzureBlobStorage blobStorage)
        {
            this.blobStorage = blobStorage;
        }

        public async Task<IActionResult> Index()
        {
            var model = new FilesViewModel();
            foreach (var item in await blobStorage.ListAsync())
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, BlobName = item.BlobName });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileInputModel inputModel)
        {
            if (inputModel.File == null)
                return Content("Argument null");

            if (inputModel.File == null || inputModel.File.Count == 0)
                return Content("file not selected");

            foreach (var item in inputModel.File)
            {
                var blobName = item.GetFilename();
                var fileStream = await item.GetFileStream();
                if (!string.IsNullOrEmpty(inputModel.Folder))
                    blobName = string.Format(@"{0}\{1}", inputModel.Folder, blobName);
                await blobStorage.UploadAsync(blobName, fileStream);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Download(string blobName, string name)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            var stream = await blobStorage.DownloadAsync(blobName);
            return File(stream.ToArray(), "application/octet-stream", name);
        }

        public async Task<IActionResult> Delete(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            await blobStorage.DeleteAsync(blobName);

            return RedirectToAction("Index");
        }
    }
}
