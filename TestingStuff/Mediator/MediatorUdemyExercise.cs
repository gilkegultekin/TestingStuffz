using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingStuff.Mediator
{
    public class MediatorUdemyExercise
    {
        public static void Test()
        {
            var mediator = new Mediator();

            var alice = new Participant(mediator) { Name = "Alice" };
            var bob = new Participant(mediator) { Name = "Bob" };
            var jake = new Participant(mediator) { Name = "Jake" };

            alice.Say(3);
            bob.Say(12);
            alice.Say(7);
            alice.Say(8);
            bob.Say(5);
            jake.Say(100);
            alice.Quit();
            jake.Say(2);
        }
    }

    public class Participant : IObserver<int>
    {
        private readonly Mediator _mediator;

        private readonly IDisposable _subTicket;

        public int Value { get; set; }

        public string Name { get; set; }

        public Participant(Mediator mediator)
        {
            // todo
            _mediator = mediator;
            _subTicket = _mediator.Subscribe(this);
        }

        public void Say(int n)
        {
            // todo
            _mediator.Publish(this, n);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(int value)
        {
            Value += value;
            Console.WriteLine($"{Name}'s value is {Value}!");
        }

        public void Quit()
        {
            _subTicket.Dispose();
        }
    }

    public class Mediator : IObservable<int>
    {
        private readonly IList<IObserver<int>> _observers = new List<IObserver<int>>();

        // todo
        public IDisposable Subscribe(IObserver<int> observer)
        {
            _observers.Add(observer);
            return new SubscriptionTicket(this, observer);
        }

        public void Publish(IObserver<int> publisher, int value)
        {
            foreach (var observer in _observers.Where(o => !o.Equals(publisher)))
            {
                observer.OnNext(value);
            }
        }

        private class SubscriptionTicket : IDisposable
        {
            private readonly Mediator _mediator;

            private readonly IObserver<int> _observer;

            public SubscriptionTicket(Mediator mediator, IObserver<int> observer)
            {
                _mediator = mediator;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_mediator._observers.Contains(_observer))
                {
                    _mediator._observers.Remove(_observer);
                }
            }
        }
    }
}