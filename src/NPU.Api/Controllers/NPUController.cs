using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NPU.Bl;
using NPU.Data.Model;
using NPU.Infrastructure.Dtos;

namespace NPU.Controllers
{
    [Route("api/npus")]
    [ApiController]
    public class NpuController(NpuService npuService) : ControllerBase
    {
        /// <summary>
        /// Create a new npu
        /// </summary>
        /// <param name="request"> The request to create a new npu </param>
        /// <returns> The created npu </returns>
        [HttpPost]
        public async Task<ActionResult<Npu>> CreateNpu(CreateNpuRequest request)
        {
            var createdNpu = await npuService.CreateNpuWithImagesAsync(request.Name, request.Description ?? "",
                request.Images.Select(i => (i.FileName, i.OpenReadStream())));

            return CreatedAtAction(
                nameof(GetNpu),
                new { id = createdNpu.Id },
                createdNpu);
        }

        /// <summary>
        /// Get a npu by id
        /// </summary>
        /// <param name="id"> The id of the npu </param>
        /// <returns> The requested npu </returns>
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

        /// <summary>
        /// Get npus in a paginated response
        /// </summary>
        /// <param name="searchTerm"> The search term to filter npus by. Default is null </param>
        /// <param name="sortOrderKey"> The key to sort by, default is CreatedAt. Can be any property of Npu </param>
        /// <param name="ascending"> Whether to sort in ascending order, default is true </param>
        /// <param name="page"> The page number to get, page starts at 1 </param>
        /// <param name="pageSize"> The number of items per page, default is 10 and max is 50 </param>
        /// <returns> A list of npus in a paginated response </returns>
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

        /// <summary>
        /// Post a score for a npu
        /// </summary>
        /// <param name="id"> The id of the npu </param>
        /// <param name="score"> The score to post </param>
        /// <returns> The posted score </returns>
        [HttpPost("{id}/score")]
        public async Task<IActionResult> ScoreNpu(string id, [FromBody] CreateScoreRequest score)
        {
            var scoreResponse = await npuService.CreateScoreOfNpuAsync(id, score);

            if (scoreResponse == null)
            {
                return BadRequest();
            }

            return Ok(scoreResponse);
        }
    }
}