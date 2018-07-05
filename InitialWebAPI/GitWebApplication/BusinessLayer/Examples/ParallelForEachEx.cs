using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{
    public class ParallelForEachEx
    {
        int[] listOfStuff = { 1, 4, 5, 6, 7, 8 };


        private int[] DoWork(int[] inputs)
        {
            Object lockMe = new Object();
            int[] arrayResults = new int[inputs.Length];

            var concurrentBagResults = new ConcurrentBag<int>(); //if we don't care about order
            var concurrentDictionaryResults = new ConcurrentDictionary<long, int>(); //

            Parallel.ForEach(inputs, (intput, state, index) =>
            {
                //do work here
                int result = 1;

                lock (lockMe)
                {
                    arrayResults[index] = result;
                }

                concurrentBagResults.Add(result);
                concurrentDictionaryResults[index] = result;
            });

            //return arrayResults;
            //return concurrentBagResults.ToArray();
            return concurrentDictionaryResults.Select(i => i.Value).ToArray();
        }

        private int[] DictionaryToArray (KeyValuePair<long, int>[] inputs)
        {
            return inputs.Select(i => i.Value).ToArray();
        }

    }
}
