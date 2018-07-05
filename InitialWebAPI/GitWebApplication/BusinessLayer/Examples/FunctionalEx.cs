using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{
    public static class FunctionalEx
    {

        public async static Task<int> FindParallelNumbers(int[] numbers)
        {
            var task = new Task<IEnumerable<int>>(
                () => FindNumbers(numbers)
            );

            return (await task).ToArray()[0];
        }

        public static int[] FindNumbers(int[] numbers)
        {
            var result = new List<int>();

            foreach (var number in numbers.Find(IsOdd).Take(2))
                result.Add(number);

            return result.ToArray();
        }


        private static IEnumerable<int> Find(this IEnumerable<int> values, Func<int, bool> test)
        {
            //var result = new List<int>();

            foreach(var number in values)
            {
                if (test(number))
                    yield return number;
            }

            //return result;
        }

        /// <summary>
        /// negatives
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(int number)
        {
            bool result = true;
            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
