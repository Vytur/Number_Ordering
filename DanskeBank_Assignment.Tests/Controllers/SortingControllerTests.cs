using Moq;
using DanskeBank_Assignment.Controllers;
using DanskeBank_Assignment.Services;
using DanskeBank_Assignment.SortingStrategies;
using Microsoft.AspNetCore.Mvc;
using DanskeBank_Assignment.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace DanskeBank_Assignment.Tests.Controllers
{
    [TestFixture]
    public class SortingControllerTests
    {
        private SortingController _controller;
        private Mock<SortingService> _sortingServiceMock;
        private Mock<IFileService> _fileServiceMock;
        private Mock<ILogger<SortingService>> _sortingServiceLoggerMock;
        private ILogger<SortingController> _logger;
        private static readonly int[] value = [1, 3, 5, 8];

        [SetUp]
        public void SetUp()
        {
            _sortingServiceLoggerMock = new Mock<ILogger<SortingService>>();
            _sortingServiceMock = new Mock<SortingService>(_sortingServiceLoggerMock.Object);

            _fileServiceMock = new Mock<IFileService>();
            _logger = new NullLogger<SortingController>();

            _controller = new SortingController(_sortingServiceMock.Object, _fileServiceMock.Object, _logger);
        }

        [TestCase(SortingAlgorithm.BubbleSort)]
        [TestCase(SortingAlgorithm.QuickSort)]
        [TestCase(SortingAlgorithm.MergeSort)]
        public void SortNumbers_ValidInput_ReturnsOk(SortingAlgorithm algorithm)
        {
            // Arrange
            var numbers = new[] { 5, 3, 8, 1 };

            _sortingServiceMock
                .Setup(service => service.MeasurePerformance(numbers, It.IsAny<ISortingStrategy>()))
                .Returns(new SortingResultModel
                {
                    Algorithm = algorithm.ToString(),
                    SortedNumbers = [1, 3, 5, 8],
                    ExecutionTime = 100,
                    MemoryUsage = 2048
                });

            var controller = new SortingController(_sortingServiceMock.Object, _fileServiceMock.Object, _logger);

            // Act
            var result = controller.SortNumbers(numbers, algorithm);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            var responseData = okResult.Value as List<SortingResultModel>;
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!, Has.Count.EqualTo(1));

            var resultEntry = responseData.First();
            Assert.That(resultEntry, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(resultEntry.Algorithm, Is.EqualTo(algorithm.ToString()));
                Assert.That(resultEntry.SortedNumbers, Is.EquivalentTo(value));
                Assert.That(resultEntry.ExecutionTime, Is.EqualTo(100));
                Assert.That(resultEntry.MemoryUsage, Is.EqualTo(2048));
            });
        }

        [Test]
        public void SortNumbers_EmptyArray_ReturnsBadRequest()
        {
            // Act
            var result = _controller.SortNumbers([], SortingAlgorithm.BubbleSort);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}