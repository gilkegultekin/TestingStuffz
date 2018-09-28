using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TestingStuff.Coordination
{
    class AsyncBarrier
    {
        //could just be a regular collection since I'm using lock instead of Interlocked
        private ConcurrentStack<TaskCompletionSource<bool>> _participantList;
        private readonly int _participantCount;
        private readonly Action _postPhaseAction;
        private int _currentParticipantCount;
        private readonly object _sync = new object();

        public AsyncBarrier(int participantCount)
        {
            _participantCount = participantCount;
            _currentParticipantCount = participantCount;
            _participantList = new ConcurrentStack<TaskCompletionSource<bool>>();
        }

        public AsyncBarrier(int participantCount, Action postPhaseAction) : this(participantCount)
        {
            _postPhaseAction = postPhaseAction;
        }

        public Task SignalAndWait()
        {
            var tcs = new TaskCompletionSource<bool>();
            
            lock (_sync)
            {
                _participantList.Push(tcs);

                if (--_currentParticipantCount > 0)
                    return tcs.Task;

                _postPhaseAction?.Invoke();

                //reset barrier state
                _currentParticipantCount = _participantCount;
                var oldParticipantList = _participantList;
                _participantList = new ConcurrentStack<TaskCompletionSource<bool>>();
                Parallel.ForEach(oldParticipantList, w => w.SetResult(true));

                return tcs.Task;
            }
        }

        //public Task SignalAndWait()
        //{
        //    var tcs = new TaskCompletionSource<bool>();
        //    _participantList.Push(tcs);

        //    if (Interlocked.Decrement(ref _currentParticipantCount) != 0) return tcs.Task;

        //    _postPhaseAction?.Invoke(this);

        //    //reset barrier state
        //    _currentParticipantCount = _participantCount;
        //    var oldWaiterList = _participantList;
        //    _participantList = new ConcurrentStack<TaskCompletionSource<bool>>();
        //    Parallel.ForEach(oldWaiterList, w => w.SetResult(true));

        //    return tcs.Task;
        //}
    }
}
