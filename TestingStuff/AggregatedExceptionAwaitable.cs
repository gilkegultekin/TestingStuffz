using System.Threading.Tasks;

namespace TestingStuff
{
    public class AggregatedExceptionAwaitable
    {
        private readonly Task _task;

        public AggregatedExceptionAwaitable(Task task)
        {
            _task = task;
        }

        public AggregatedExceptionAwaiter GetAwaiter()
        {
            return new AggregatedExceptionAwaiter(_task);
        }
    }
}
