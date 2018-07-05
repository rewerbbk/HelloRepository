using System.Collections.Generic;

namespace Utility.Extensions
{
    public static class ListExtensions
    {
        public static int LinearSearch<T>(this List<T> list, T item, IComparer<T> comparer)
        {
            int index = 0;
            bool found = false;
            foreach (T hhm in list)
            {
                if (comparer.Compare(hhm, item) == 0)
                {
                    found = true;
                    break;
                }

                index++;
            }
            if (!found)
                index = index * -1;

            return index;
        }
    }

}