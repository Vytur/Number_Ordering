namespace DanskeBank_Assignment.SortingStrategies
{
    public class QuickSortStrategy : ISortingStrategy
    {
        public IEnumerable<int> Sort(IEnumerable<int> numbers)
        {
            if (numbers.Count() <= 1)
                return numbers;

            var pivot = numbers.ElementAt(numbers.Count() / 2);
            var left = numbers.Where(x => x < pivot);
            var right = numbers.Where(x => x > pivot);

            return Sort(left).Concat([pivot]).Concat(Sort(right));
        }
    }
}
