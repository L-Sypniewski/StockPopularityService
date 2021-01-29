using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class
        TaskExtension //TODO: Move to a shared library https://github.com/StephenCleary/AsyncEx/blob/master/doc/TaskExtensions.md
    {
        public static async IAsyncEnumerable<T> ParallelEnumerateAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            var remaining = new List<Task<T>>(tasks);

            while (remaining.Count != 0)
            {
                var task = await Task.WhenAny(remaining);
                remaining.Remove(task);
                yield return await task;
            }
        }
    }
}