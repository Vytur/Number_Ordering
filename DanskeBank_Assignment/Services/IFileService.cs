namespace DanskeBank_Assignment.Services
{
    public interface IFileService
    {
        void Save(string content, string fileName = "result.txt");
        string Load(string fileName = "result.txt");
    }
}
