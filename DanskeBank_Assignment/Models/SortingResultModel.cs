namespace DanskeBank_Assignment.Models
{
    public class SortingResultModel
    {
        public required string Algorithm { get; set; }
        public required IEnumerable<int> SortedNumbers { get; set; }
        public long? ExecutionTime { get; set; }
        public long? MemoryUsage { get; set; }
    }
}
