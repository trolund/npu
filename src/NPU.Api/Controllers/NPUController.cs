using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPU.Bl;

namespace NPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPUController(NotesService notesService) : ControllerBase
    {
        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file selected");
            }

            // Ensure the uploads directory exists
            if (!Directory.Exists(""))
            {
                Directory.CreateDirectory("");
            }

            // Generate a unique filename to avoid conflicts
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine("", fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok($"File uploaded successfully: {fileName}");
        }
        
        
        [HttpGet]
        public async Task<ActionResult> GetNote()
        {
            return Ok("Works!!!!");
        }
    }
}