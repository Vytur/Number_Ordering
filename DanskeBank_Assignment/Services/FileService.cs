
namespace DanskeBank_Assignment.Services
{
    public class FileService(ILogger<SortingService> logger) : IFileService
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "result.txt");

        private readonly ILogger<SortingService> _logger = logger;

        public void Save(string content, string fileName = "result.txt")
        {
            try
            {
                _logger.LogInformation("Saving content to file: {FileName}", fileName);

                File.WriteAllText(_filePath, content);

                _logger.LogInformation("File saved successfully at path: {FilePath}", _filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the file: {FileName}", fileName);
                throw;
            }
        }

        public string Load(string fileName = "result.txt")
        {
            try
            {
                _logger.LogInformation("Loading content from file: {FileName}", fileName);

                if (!File.Exists(_filePath))
                {
                    _logger.LogWarning("File not found at path: {FilePath}", _filePath);
                    throw new FileNotFoundException("No saved file found.");
                }

                string content = File.ReadAllText(_filePath);

                _logger.LogInformation("File loaded successfully from path: {FilePath}", _filePath);
                return content;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "File not found: {FileName}", fileName);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading the file: {FileName}", fileName);
                throw;
            }
        }
    }
}
