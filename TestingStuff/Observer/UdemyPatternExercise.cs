using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace TestingStuff.Observer
{
    public class UdemyPatternExercise
    {
        public static void DoWork()
        {
            var game = new Game();

            var rat = new Rat(game, "rat1");
            //Console.WriteLine($"{rat.Name} has attack {rat.Attack}");
            rat.Attack.Should().Be(1);

            var rat2 = new Rat(game, "rat2");
            //Console.WriteLine($"{rat.Name} has attack {rat.Attack}");
            rat.Attack.Should().Be(2);
            //Console.WriteLine($"{rat2.Name} has attack {rat2.Attack}");
            rat2.Attack.Should().Be(2);

            using (var rat3 = new Rat(game, "rat3"))
            {
                //Console.WriteLine($"{rat.Name} has attack {rat.Attack}");
                //Console.WriteLine($"{rat2.Name} has attack {rat2.Attack}");
                //Console.WriteLine($"{rat3.Name} has attack {rat3.Attack}");

                rat.Attack.Should().Be(3);
                rat2.Attack.Should().Be(3);
                rat3.Attack.Should().Be(3);
            }

            //Console.WriteLine($"{rat.Name} has attack {rat.Attack}");
            //Console.WriteLine($"{rat2.Name} has attack {rat2.Attack}");
            rat.Attack.Should().Be(2);
            rat2.Attack.Should().Be(2);
        }
    }

    public class Game
    {
        // todo
        // remember - no fields or properties!

        public void EnterRat(Rat rat)
        {
            RatInfoProvider.Instance.TrackRats(RatActionType.Enter);
        }

        public void ExitRat(Rat rat)
        {
            RatInfoProvider.Instance.TrackRats(RatActionType.Leave);
        }
    }

    public class Rat : IObserver<RatInfo>, IDisposable
    {
        public int Attack = 1;

        private readonly Game _game;

        private readonly IDisposable _unsubscriber;

        public string Name { get; }

        public Rat(Game game, string name)
        {
            // todo
            _game = game;
            Name = name;

            _unsubscriber = RatInfoProvider.Instance.Subscribe(this);

            Console.WriteLine($"{Name} is entering the game");

            _game.EnterRat(this);

            Console.WriteLine($"{Name} has entered the game");
        }


        public void Dispose()
        {
            _unsubscriber.Dispose();

            Console.WriteLine($"{Name} is leaving the game");

            _game.ExitRat(this);

            Console.WriteLine($"{Name} has left the game");
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(RatInfo value)
        {
            Attack = value.NumberOfRats;
            //Console.WriteLine($"{Name} has attack value {Attack}");
        }
    }

    public class RatInfo
    {
        public int NumberOfRats { get; }

        public RatInfo(int numberOfRats)
        {
            NumberOfRats = numberOfRats;
        }
    }

    public class RatInfoProvider : IObservable<RatInfo>
    {
        private static RatInfoProvider _instance;

        public static RatInfoProvider Instance => _instance ?? (_instance = new RatInfoProvider());

        private readonly IList<IObserver<RatInfo>> _observers;

        private RatInfoProvider()
        {
            _observers = new List<IObserver<RatInfo>>();
        }

        private int _numberOfRats;

        public void TrackRats(RatActionType action)
        {
            var info = action == RatActionType.Enter ? new RatInfo(++_numberOfRats) : new RatInfo(--_numberOfRats);

            foreach (var observer in _observers)
            {
                observer.OnNext(info);
            }
        }

        public IDisposable Subscribe(IObserver<RatInfo> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(this, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly RatInfoProvider _provider;

            private readonly IObserver<RatInfo> _observer;

            public Unsubscriber(RatInfoProvider provider, IObserver<RatInfo> observer)
            {
                _provider = provider;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_provider._observers.Contains(_observer))
                {
                    _provider._observers.Remove(_observer);
                }
            }
        }
    }

    public enum RatActionType
    {
        Enter,
        Leave
    }
}
