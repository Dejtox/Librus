using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            var path = Path.Combine("StaticFiles/Images",fileName) ;
            if(System.IO.File.Exists(path))
            {
                var imagebytes = System.IO.File.ReadAllBytes(path);
                return File(imagebytes, "image/jpg");
            }
            return NotFound();


        }
        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                  // var fileName = "TempName_0.jpg";
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpDelete("{imageName}")]
        public IActionResult DeleteImage(string imageName)
        {
            var imagePath = Path.Combine("StaticFiles/Images", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return Ok($"Plik {imageName} został usunięty.");
            }

            return NotFound($"Plik {imageName} nie istnieje.");
        }
        [HttpPatch("{oldImageName}/{newImageName}")]
        public IActionResult RenameImage(string oldImageName, string newImageName)
        {
            var oldImagePath = Path.Combine("StaticFiles/Images", oldImageName);
            var newImagePath = Path.Combine("StaticFiles/Images", newImageName);

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Move(oldImagePath, newImagePath);
                return Ok($"Plik {oldImageName} został przemianowany na {newImageName}.");
            }

            return NotFound($"Plik {oldImageName} nie istnieje.");
        }
    }
}
