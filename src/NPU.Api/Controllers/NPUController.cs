using Microsoft.AspNetCore.Mvc;
using NPU.Bl;
using NPU.Infrastructure.Dtos;

namespace NPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpuController(NpuService npuService, FileUploadService fileUploadService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNpu(CreateNpuRequest request)
        {
            var fileName = await fileUploadService.UploadFileAsync(request.File.FileName, request.File.OpenReadStream());
            return Ok($"File uploaded successfully: {fileName}");
        }

        [HttpGet]
        public async Task<ActionResult> GetNote()
        {
            return Ok("Works!!!!");
        }
    }
}