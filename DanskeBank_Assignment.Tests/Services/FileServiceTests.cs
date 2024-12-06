using DanskeBank_Assignment.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace DanskeBank_Assignment.Tests.Services
{
    [TestFixture]
    internal class FileServiceTests
    {
        private Mock<ILogger<SortingService>> _loggerMock;
        private FileService _fileService;
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<SortingService>>();
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt");
            _fileService = new FileService(_loggerMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [Test]
        public void Save_SuccessfullyWritesContentToFile()
        {
            // Arrange
            string content = "1 3 5 8";

            // Act
            _fileService.Save(content, "result.txt");

            // Assert
            Assert.That(File.Exists(_filePath), Is.True);

            var fileContent = File.ReadAllText(_filePath);
            Assert.That(fileContent, Is.EqualTo(content));
        }

        [Test]
        public void Load_SuccessfullyLoadsContentFromFile()
        {
            // Arrange
            string expectedContent = "1 3 5 8";
            File.WriteAllText(_filePath, expectedContent);

            // Act
            string actualContent = _fileService.Load("result.txt");

            // Assert
            Assert.That(actualContent, Is.EqualTo(expectedContent));
        }

        [Test]
        public void Load_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            string fileName = "nonexistent.txt";

            // Act & Assert
            var ex = Assert.Throws<FileNotFoundException>(() => _fileService.Load(fileName));
            Assert.That(ex.Message, Is.EqualTo("No saved file found."));
        }
    }
}
