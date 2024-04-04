using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace MyApp.Namespace
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider 
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId) {
            var pathTofile = "csharp-icon.svg";

            if (!System.IO.File.Exists(pathTofile)) {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathTofile, out var contentType)) { 
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(pathTofile);
            return File(bytes, contentType, Path.GetFileName(pathTofile));
        }
    }
}
