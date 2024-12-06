using DanskeBank_Assignment.Services;
using DanskeBank_Assignment.SortingStrategies;
using Microsoft.Extensions.Logging;
using Moq;

namespace DanskeBank_Assignment.Tests.Services
{
    [TestFixture]
    internal class SortingServiceTests
    {
        private SortingService _sortingService;
        private Mock<ILogger<SortingService>> _loggerMock;
        internal static readonly int[] expected = [1, 3, 5, 8];

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<SortingService>>();
            _sortingService = new SortingService(_loggerMock.Object);
        }

        [TestCase(typeof(BubbleSortStrategy), "BubbleSortStrategy")]
        [TestCase(typeof(QuickSortStrategy), "QuickSortStrategy")]
        [TestCase(typeof(MergeSortStrategy), "MergeSortStrategy")]
        public void MeasureSortingPerformance_WithValidInput_ReturnsSortedNumbersAndPerformanceMetrics(Type strategyType, string algorithmName)
        {
            // Arrange
            var numbers = new[] { 5, 3, 8, 1 };

            var sortingStrategy = Activator.CreateInstance(strategyType) as ISortingStrategy;

            if (sortingStrategy == null)
            {
                Assert.Fail("Strategy should not be null.");
            }

            // Act
            var sortedResult = _sortingService.MeasurePerformance(numbers, sortingStrategy);

            Assert.Multiple(() =>
            {
                Assert.That(sortedResult.SortedNumbers, Is.EqualTo(expected));

                Assert.That(sortedResult.Algorithm, Is.EqualTo(algorithmName));

                Assert.That(sortedResult.ExecutionTime, Is.InstanceOf<long>(), "Execution time should be a number.");
                Assert.That(sortedResult.MemoryUsage, Is.InstanceOf<long>(), "Memory usage should be a number.");
            });
        }

        [TestCase(typeof(BubbleSortStrategy), "BubbleSortStrategy")]
        [TestCase(typeof(QuickSortStrategy), "QuickSortStrategy")]
        [TestCase(typeof(MergeSortStrategy), "MergeSortStrategy")]
        public void MeasureSortingPerformance_WithEmptyInput_ReturnsEmptyList(Type strategyType, string algorithmName)
        {
            // Arrange
            var numbers = Array.Empty<int>();
            var sortingStrategy = Activator.CreateInstance(strategyType) as ISortingStrategy;

            // Act
            var sortedResult = _sortingService.MeasurePerformance(numbers, sortingStrategy);

            if (sortingStrategy == null)
            {
                Assert.Fail("Strategy should not be null.");
            }


            Assert.Multiple(() =>
            {
                Assert.That(sortedResult.SortedNumbers, Is.Empty);

                Assert.That(sortedResult.Algorithm, Is.EqualTo(algorithmName));

                Assert.That(sortedResult.ExecutionTime, Is.InstanceOf<long>(), "Execution time should be a number.");
                Assert.That(sortedResult.MemoryUsage, Is.InstanceOf<long>(), "Memory usage should be a number.");
            });
        }
    }
}
