namespace DanskeBank_Assignment.SortingStrategies
{
    public class BubbleSortStrategy : ISortingStrategy
    {
        public IEnumerable<int> Sort(IEnumerable<int> numbers)
        {
            var list = numbers.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    }
                }
            }
            return list;
        }
    }
}
