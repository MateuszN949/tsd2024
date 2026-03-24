using System.Collections;

namespace GoldSavings.App.Task2_2
{
    public class RandomList<T>(IEnumerable<T> values) : IEnumerable<T>
    {
        private readonly List<T> items = [.. values];
        private readonly Random random = new();

        public void Add(T element)
        {
            if (random.Next(2) == 0)
            {
                items.Add(element);
            }
            else
            {
                items.Insert(0, element);
            }
        }

        public T Get(int idx)
        {
            if (IsEmpty())
                throw new InvalidOperationException();

            int maxIndex = Math.Min(idx, items.Count - 1);
            return items[random.Next(maxIndex + 1)];
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}