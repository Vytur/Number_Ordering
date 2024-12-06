namespace DanskeBank_Assignment.SortingStrategies
{
    public class MergeSortStrategy : ISortingStrategy
    {
        public IEnumerable<int> Sort(IEnumerable<int> numbers)
        {
            if (numbers.Count() <= 1)
                return numbers;

            var middle = numbers.Count() / 2;
            var left = numbers.Take(middle);
            var right = numbers.Skip(middle);

            return Merge(Sort(left).ToList(), Sort(right).ToList());
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i] < right[j])
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));

            return result;
        }
    }
}
