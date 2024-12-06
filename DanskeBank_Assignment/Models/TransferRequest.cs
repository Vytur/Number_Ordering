using System.ComponentModel.DataAnnotations;

namespace DanskeBank_Assignment.Models
{
    public class TransferRequest
    {
        [Required]
        public int[]? Numbers { get; set; }

        public SortingAlgorithm Algorithm { get; set; } = SortingAlgorithm.QuickSort;
    }
}
