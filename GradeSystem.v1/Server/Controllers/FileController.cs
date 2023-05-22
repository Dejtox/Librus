using GradeSystem.v1.Server.Data;
using GradeSystem.v1.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlazorFileUpload.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly GradeSystemv1ServerContext _context;
        private readonly IWebHostEnvironment _webhost;

        public FileController(IWebHostEnvironment env, GradeSystemv1ServerContext context, IWebHostEnvironment webhost)
        {
            _env = env;
            _context = context;
            _webhost = webhost;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var uploadResult = await _context.Uploads.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));

            var path = Path.Combine(_env.ContentRootPath, "uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, uploadResult.ContentType, Path.GetFileName(path));
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files,string name)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                var path = Path.Combine(_webhost.WebRootPath, "Img", name);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

            }

            await _context.SaveChangesAsync();

            return Ok(uploadResults);
        }
    }
}