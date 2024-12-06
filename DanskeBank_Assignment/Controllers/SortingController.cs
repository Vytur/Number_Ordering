using DanskeBank_Assignment.Models;
using DanskeBank_Assignment.Services;
using DanskeBank_Assignment.SortingStrategies;
using Microsoft.AspNetCore.Mvc;

namespace DanskeBank_Assignment.Controllers
{
    [ApiController]
    [Route("api/numbers")]
    public class SortingController(SortingService sortingService, IFileService fileService, ILogger<SortingController> logger) : ControllerBase
    {
        private readonly SortingService _sortingService = sortingService;
        private readonly IFileService _fileService = fileService;
        private readonly ILogger<SortingController> _logger = logger;

        [HttpPost("sort")]
        public IActionResult SortNumbers([FromBody] int[] numbers, [FromQuery] SortingAlgorithm? algorithm = null)
        {
            if (numbers == null || numbers.Length == 0)
            {
                _logger.LogWarning("Invalid sorting request received.");
                return BadRequest("Numbers cannot be null or empty.");
            }

            _logger.LogInformation("Sorting request received with {Count} numbers.", numbers.Length);

            try
            {
                var algorithmsToSort = algorithm.HasValue ? [algorithm.Value] : Enum.GetValues<SortingAlgorithm>().Cast<SortingAlgorithm>();

                var sortingResults = new List<SortingResultModel>();

                foreach (var alg in algorithmsToSort)
                {
                    ISortingStrategy sortingStrategy = alg switch
                    {
                        SortingAlgorithm.BubbleSort => new BubbleSortStrategy(),
                        SortingAlgorithm.QuickSort => new QuickSortStrategy(),
                        SortingAlgorithm.MergeSort => new MergeSortStrategy(),
                        _ => throw new NotSupportedException("Algorithm not supported")
                    };

                    var sortingResult = _sortingService.MeasurePerformance(numbers, sortingStrategy);

                    _fileService.Save(string.Join(" ", sortingResult.SortedNumbers));

                    sortingResults.Add(sortingResult);
                }

                _logger.LogInformation("Sorting and file save successful.");

                return Ok(sortingResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the sorting request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("latest")]
        public IActionResult GetLatestFileContent()
        {
            try
            {
                var content = _fileService.Load();
                return Ok(new { Content = content });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
