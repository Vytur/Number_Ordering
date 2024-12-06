using DanskeBank_Assignment.Models;
using DanskeBank_Assignment.SortingStrategies;
using System.Diagnostics;

namespace DanskeBank_Assignment.Services
{
    public class SortingService(ILogger<SortingService> logger)
    {
        private readonly ILogger<SortingService> _logger = logger;


        public virtual SortingResultModel MeasurePerformance(IEnumerable<int> numbers, ISortingStrategy strategy)
        {
            _logger.LogInformation("Sort method called with algorithm: {algorithm}", strategy.GetType().Name);

            var stopwatch = Stopwatch.StartNew();

            long memoryBefore = GC.GetTotalMemory(false);

            var sortedNumbers = strategy.Sort(numbers);

            stopwatch.Stop();

            long memoryAfter = GC.GetTotalMemory(false);

            long memoryUsed = (memoryAfter - memoryBefore); // in KB / 1024

            _logger.LogInformation("Sorting completed with {algorithm}. Time: {time} ns, Memory Used: {memory} Bytes", strategy.GetType().Name, stopwatch.ElapsedTicks, memoryUsed);

            return new SortingResultModel
            {
                Algorithm = strategy.GetType().Name,
                SortedNumbers = sortedNumbers,
                ExecutionTime = stopwatch.ElapsedMilliseconds,
                MemoryUsage = memoryUsed
            };           
        }
    }
}
