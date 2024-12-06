using DanskeBank_Assignment.SortingStrategies;

namespace DanskeBank_Assignment.Tests.SortingStrategies
{
    [TestFixture]
    internal class SortingStrategyTests
    {
        private readonly ISortingStrategy[] _sortingStrategies =
        [
            new MergeSortStrategy(),
            new BubbleSortStrategy(),
            new QuickSortStrategy()
        ];

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public void SortingStrategy_SortsNumbersCorrectly(ISortingStrategy sortingStrategy, List<int> input, List<int> expected)
        {
            // Act
            var sortedNumbers = sortingStrategy.Sort(input);

            // Assert
            Assert.That(sortedNumbers.ToList(), Is.EqualTo(expected));
        }

        static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(new MergeSortStrategy(), new List<int> { 5, 3, 8, 4, 2 }, new List<int> { 2, 3, 4, 5, 8 }).SetName("MergeSort_SortsNumbersCorrectly");
            yield return new TestCaseData(new BubbleSortStrategy(), new List<int> { 5, 3, 8, 4, 2 }, new List<int> { 2, 3, 4, 5, 8 }).SetName("BubbleSort_SortsNumbersCorrectly");
            yield return new TestCaseData(new QuickSortStrategy(), new List<int> { 5, 3, 8, 4, 2 }, new List<int> { 2, 3, 4, 5, 8 }).SetName("QuickSort_SortsNumbersCorrectly");

            yield return new TestCaseData(new MergeSortStrategy(), new List<int> { }, new List<int> { }).SetName("MergeSort_HandlesEmptyList");
            yield return new TestCaseData(new BubbleSortStrategy(), new List<int> { }, new List<int> { }).SetName("BubbleSort_HandlesEmptyList");
            yield return new TestCaseData(new QuickSortStrategy(), new List<int> { }, new List<int> { }).SetName("QuickSort_HandlesEmptyList");

            yield return new TestCaseData(new MergeSortStrategy(), new List<int> { 5 }, new List<int> { 5 }).SetName("MergeSort_SortsSingleElementList");
            yield return new TestCaseData(new BubbleSortStrategy(), new List<int> { 5 }, new List<int> { 5 }).SetName("BubbleSort_SortsSingleElementList");
            yield return new TestCaseData(new QuickSortStrategy(), new List<int> { 5 }, new List<int> { 5 }).SetName("QuickSort_SortsSingleElementList");

            yield return new TestCaseData(new MergeSortStrategy(), new List<int> { -5, -3, -8, -4, -2 }, new List<int> { -8, -5, -4, -3, -2 }).SetName("MergeSort_SortsNegativeNumbers");
            yield return new TestCaseData(new BubbleSortStrategy(), new List<int> { -5, -3, -8, -4, -2 }, new List<int> { -8, -5, -4, -3, -2 }).SetName("BubbleSort_SortsNegativeNumbers");
            yield return new TestCaseData(new QuickSortStrategy(), new List<int> { -5, -3, -8, -4, -2 }, new List<int> { -8, -5, -4, -3, -2 }).SetName("QuickSort_SortsNegativeNumbers");
        }
    }
}
