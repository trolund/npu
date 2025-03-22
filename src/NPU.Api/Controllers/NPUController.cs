using Microsoft.AspNetCore.Mvc;
using NPU.Bl;
using NPU.Data.Model;
using NPU.Infrastructure.Dtos;

namespace NPU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpuController(NpuService npuService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNpu(CreateNpuRequest request)
        {
            var createdNpu = await npuService.CreateNpuWithImagesAsync(request.Name, request.Description ?? "",
                request.Images.Select(i => (i.FileName, i.OpenReadStream())));

            // TODO link to the created NPU
            return Created($"/api/npu/{createdNpu.Id}", createdNpu);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<Npu>>> GetNpus(
            string? searchTerm,
            string? sortOrderKey,
            bool ascending = true,
            int page = 1, 
            int pageSize = 10
        )
        {
            return Ok(await npuService.GetNpuPaginatedAsync(searchTerm, page, pageSize, ascending, sortOrderKey));
        }
    }
}