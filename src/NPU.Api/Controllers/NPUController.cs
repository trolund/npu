using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NPU.Bl;
using NPU.Data.Model;
using NPU.Infrastructure.Dtos;

namespace NPU.Controllers
{
    [Route("v1/api/npus")]
    [ApiController]
    public class NpuController(NpuService npuService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNpu(CreateNpuRequest request)
        {
            var createdNpu = await npuService.CreateNpuWithImagesAsync(request.Name, request.Description ?? "",
                request.Images.Select(i => (i.FileName, i.OpenReadStream())));

            return CreatedAtAction(
                nameof(GetNpu),
                new { id = createdNpu.Id },
                createdNpu);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NpuResponse>>> GetNpu(string id)
        {
            var item = await npuService.GetNpuAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<NpuResponse>>> GetNpus(
            string? searchTerm,
            string sortOrderKey = nameof(Npu.CreatedAt),
            bool ascending = true,
            [Range(1, int.MaxValue)] int page = 1,
            [Range(1, 50)] int pageSize = 10
        )
        {
            return Ok(await npuService.GetNpuPaginatedAsync(searchTerm, page, pageSize, ascending, sortOrderKey));
        }

        // give score to npu
        [HttpPost("{id}/score")]
        public async Task<IActionResult> ScoreNpu(string id, [FromBody] CreateScoreRequest score)
        {
            var scoreResponse = await npuService.GiveScoreAsync(id, score);

            if (scoreResponse == null)
            {
                return BadRequest();
            }

            return Ok(scoreResponse);
        }
    }
}