using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NPU.Bl;
using NPU.Data.Models;
using NPU.Infrastructure.Dtos;

namespace NPU.Controllers
{
    [Route("v1/api/npus")]
    [ApiController]
    public class NpuController(INpuService npuService) : ControllerBase
    {
        /// <summary>
        /// Create a new npu
        /// </summary>
        /// <param name="request"> The request to create a new npu </param>
        /// <returns> The created npu </returns>
        /// <response code="201"> Returns the newly created npu </response>
        /// <response code="400"> If the request is invalid </response>
        [HttpPost]
        public async Task<ActionResult<NpuResponse>> CreateNpu(CreateNpuRequest request)
        {
            var createdNpu = await npuService.CreateNpuWithImagesAsync(request.Name, request.Description ?? "",
                request.Images.Select(i => (i.FileName, i.OpenReadStream())));

            if (createdNpu is null)
            { 
                return BadRequest("Failed to create npu");
            }

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
        /// <response code="200"> Returns the requested npu </response>
        /// <response code="404"> If the npu is not found </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<NpuResponse>> GetNpu(string id)
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
        /// <response code="200"> Returns the paginated npus - May be empty </response>
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
        /// <response code="200"> Returns the posted score </response>
        /// <response code="400"> If the request is invalid </response>
        [HttpPost("{id}/score")]
        public async Task<ActionResult<ScoreResponse>> ScoreNpu(string id, [FromBody] CreateScoreRequest score)
        {
            var scoreResponse = await npuService.CreateScoreOfNpuAsync(id, score);

            if (scoreResponse == null)
            {
                return BadRequest();
            }

            return Ok(scoreResponse);
        }
        
        /// <summary>
        /// Get image of an NPU
        /// </summary>
        /// <param name="id"> The id of the npu </param>
        /// <param name="filename"> The filename of the image </param>
        /// <returns> The image </returns>
        /// <response code="200"> Returns the image </response>
        /// <response code="404"> If the image is not found </response>
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(string id, string filename)
        {
            var (stream, fileType) = await npuService.GetImageOfNpu(id, filename);
            
            if (stream == null || fileType == null)
            {
                return NotFound("Image not found");
            }
            
            return File(stream, $"image/{fileType}");
        }
    }
}