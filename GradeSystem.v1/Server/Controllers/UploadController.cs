using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GradeSystem.v1.Server.Controllers
{
    [Authorize]
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
                if (file == null || file.Length == 0)
                    return BadRequest("Nie przesłano pliku.");


                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var rawName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                if (string.IsNullOrWhiteSpace(rawName))
                    return BadRequest("Brak nazwy pliku.");

                var fileName = Path.GetFileName(rawName);
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName); 
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(dbPath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
