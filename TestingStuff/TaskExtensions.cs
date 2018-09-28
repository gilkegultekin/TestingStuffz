using System.Threading.Tasks;

namespace TestingStuff
{
    public static class TaskExtensions
    {
        public static AggregatedExceptionAwaitable WithAggregatedExceptions(this Task task)
        {
            return new AggregatedExceptionAwaitable(task);
        }
    }
}
