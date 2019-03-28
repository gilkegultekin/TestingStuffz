using FluentAssertions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestingStuff.Observer
{
    public class UdemyPatternExercise
    {
        public static async Task DoWork()
        {
            var task1 = Task.Run(() => SingleRatTest());
            var task2 = Task.Run(() => TwoRatTest());
            var task3 = Task.Run(() => ThreeRatsOneDies());

            try
            {
                await Task.WhenAll(task1, task2, task3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public static void SingleRatTest()
        {
            var game = new Game();
            var rat = new Rat(game);
            rat.Attack.Should().Be(1);
        }

        public static void TwoRatTest()
        {
            var game = new Game();
            var rat = new Rat(game);
            var rat2 = new Rat(game);
            rat.Attack.Should().Be(2);
            rat2.Attack.Should().Be(2);
        }

        public static void ThreeRatsOneDies()
        {
            var game = new Game();

            var rat = new Rat(game);
            rat.Attack.Should().Be(1);

            var rat2 = new Rat(game);
            rat.Attack.Should().Be(2);
            rat2.Attack.Should().Be(2);

            using (var rat3 = new Rat(game))
            {
                rat.Attack.Should().Be(3);
                rat2.Attack.Should().Be(3);
                rat3.Attack.Should().Be(3);
            }

            rat.Attack.Should().Be(2);
            rat2.Attack.Should().Be(2);
        }
    }

    public class Game
    {
        // todo
        // remember - no fields or properties!

        public Game()
        {
            RatInfoProviderMapper.Instance.TrackNewGame(this);
        }

        public void EnterRat(Rat rat)
        {
            RatInfoProviderMapper.Instance[this].TrackRats(RatActionType.Enter);
        }

        public void ExitRat(Rat rat)
        {
            RatInfoProviderMapper.Instance[this].TrackRats(RatActionType.Leave);
        }
    }

    public class Rat : IObserver<RatInfo>, IDisposable
    {
        public int Attack = 1;

        private readonly Game _game;

        private readonly IDisposable _unsubscriber;

        public string Name { get; }

        public Rat(Game game, string name = null)
        {
            // todo

            _game = game;
            Name = name ?? Guid.NewGuid().ToString();

            _unsubscriber = RatInfoProviderMapper.Instance[game].Subscribe(this);

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

    public class RatInfoProviderMapper
    {
        private static RatInfoProviderMapper _instance;

        private static readonly object SyncRoot = new object();

        public static RatInfoProviderMapper Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RatInfoProviderMapper();
                    }
                }

                return _instance;
            }
        }
            
        //=> _instance ?? (_instance = new RatInfoProviderMapper());

        private readonly ConcurrentDictionary<Game, RatInfoProvider> _gameProviderMapping;

        private RatInfoProviderMapper()
        {
            _gameProviderMapping = new ConcurrentDictionary<Game, RatInfoProvider>();
        }

        public void TrackNewGame(Game game)
        {
            _gameProviderMapping[game] = new RatInfoProvider();
        }

        public RatInfoProvider this[Game game] => _gameProviderMapping[game];
    }

    public class RatInfoProvider : IObservable<RatInfo>
    {
        //private static RatInfoProvider _instance;

        //public static RatInfoProvider Instance => _instance ?? (_instance = new RatInfoProvider());

        private readonly IList<IObserver<RatInfo>> _observers;

        public RatInfoProvider()
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
