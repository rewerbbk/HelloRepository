using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Examples
{
    public class ParallelTasks
    {
        public async Task<List<int>> ProcessTasks(string[] inputs)
        {
            List<int> results = new List<int>();
            // build list of tasks to execute
            IEnumerable<Task<int>> tasksQueries = from input in inputs select ProcessSomething(input);

            // Use ToList to execute the query and start the tasks.
            List<Task<int>> tasks = tasksQueries.ToList();

            // Add a loop to process the tasks one at a time until none remain.
            while (tasks.Count > 0)
            {
                // Identify the first task that completes.
                using (Task<int> finishedTask = await Task.WhenAny(tasks))
                {
                    // Remove the selected task from the list so that you don't process it more than once.
                    tasks.Remove(finishedTask);

                    results.Add(await finishedTask);

                    // do something with individual result
                }
            }

            // do other work with all results

            return results;
        }

        private async Task<int> ProcessSomething(string input)
        {
            return await Task.Run(() => { return int.Parse(input); });
        }
    }
}
