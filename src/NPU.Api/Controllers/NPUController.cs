using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPU.Bl;

namespace NPU.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NPUController(NotesService notesService) : ControllerBase
    {
        
        // endpoint for uploading image
        [HttpPost("upload")]
        public async Task<ActionResult> UploadImage([FromForm] IFormFile file)
        {
            //var image = await notesService.UploadImageAsync(file);
            return Ok("ok");
        }
        
        
        [HttpGet]
        public async Task<ActionResult> GetNote()
        {
            return Ok("Works!!!!");
        }
    }
}