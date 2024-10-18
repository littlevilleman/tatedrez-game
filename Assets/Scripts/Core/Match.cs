using UnityEngine;

namespace Core
{
    public interface ITurnDispatcher
    {
        public void DispatchTurn(IPlayer player);
    }

    public interface IMatch : ITurnDispatcher
    {
        public event StepTurn OnStepTurn;
        public event EndMatch OnEnd;

        public IBoard Board { get; }
        public IPlayer[] Players { get; }
        public IPlayer CurrentPlayer { get; }
        public void Launch();
    }

    public class Match : IMatch
    {
        public event StepTurn OnStepTurn;
        public event EndMatch OnEnd;
        public IBoard Board { get; private set; }
        public IPlayer[] Players { get; private set; }
        public IPlayer CurrentPlayer => Players[currentPlayer];

        private int currentPlayer;
        private int turn = 0;

        public Match(IBoard boardSetup, IPlayer localPlayer, IPlayer remotePlayer)
        {
            Board = boardSetup;
            Players = new IPlayer[2] { localPlayer, remotePlayer };
        }

        public void Launch()
        {
            turn = 0;
            currentPlayer = Random.Range(0, Players.Length);
            DispatchTurn(CurrentPlayer);
            Debug.Log($"[Core/GameMatch] - Start");
        }

        public void DispatchTurn(IPlayer player)
        {
            if (player != CurrentPlayer)
                return;

            if (TatedrezUtils.CheckPlayerVictory(Board, CurrentPlayer))
            {
                OnEnd?.Invoke(CurrentPlayer);
                Debug.Log($"[Core/GameMatch] - End - {CurrentPlayer.Name} wins!");
                return;
            }

            turn++;
            currentPlayer = (currentPlayer + 1) % 2;
            CurrentPlayer.OpenTurn(this, Board);
            OnStepTurn?.Invoke(CurrentPlayer);
            Debug.Log($"[Core/GameMatch] - Launch turn {turn}- {CurrentPlayer.Name}");
        }
    }
}