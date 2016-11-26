using System;
using System.Collections.Generic;
using Xunit;
using ZNetMonad.State;

namespace ZNetMonad.Tests
{
    public sealed class StateTests
    {
        private static readonly IDictionary<char, Func<GameState, GameState>> _stateMap =
            new Dictionary<char, Func<GameState, GameState>>
        {
            { 'a', s => new GameState(s.On, s.Score + 1) },
            { 'b', s => new GameState(s.On, s.Score - 1) },
            { 'c', s => new GameState(!s.On, s.Score) }
        };

        private static GameState StartingState => new GameState(false, 0);

        [Theory]
        [InlineData("abcaaacbbcabbab", true, 0)]
        [InlineData("aaa_", false, 3)]
        public void CanPlayGame(string data, bool expectedOn, int expectedScore)
        {
            var finalState = Play(data).Eval(StartingState);

            Assert.Equal(new GameState(expectedOn, expectedScore), finalState);
        }

        private static State<GameState, GameState> Play(string data)
        {
            if (string.IsNullOrEmpty(data))
                return State<GameState, GameState>.Get();

            return from s1 in State<GameState, GameState>.Get()
                   from ignored in State<GameState, GameState>.Put(GetNextState(data[0], s1))
                   from s in Play(data.Substring(1))
                   select s;
        }

        private static GameState GetNextState(char c, GameState s)
            => _stateMap.ContainsKey(c) ? _stateMap[c](s) : s;

        private struct GameState : IEquatable<GameState>
        {
            public GameState(bool on, int score)
            {
                On = on;
                Score = score;
            }

            public bool On { get; }
            public int Score { get; }

            public bool Equals(GameState other)
                => On == other.On && Score == other.Score;

            public override string ToString()
                => $"(On: {On}, Score: {Score})";
        }
    }
}
