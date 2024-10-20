using System.Linq;
using UnityEngine;

namespace Core
{
    public interface IMatch
    {
        public event StartMatch OnStart;
        public event SwitchTurn OnSwitchTurn;
        public event EndMatch OnEnd;

        public IPlayer[] Players { get; }
        public IMatchRequestHandler LocalPlayer { get; }
        public void Launch();
        public void RequestMovement(IPlayer player, IPiece piece, Vector2Int movement);
    }

    public class Match : IMatch
    {
        public event StartMatch OnStart;
        public event SwitchTurn OnSwitchTurn;
        public event EndMatch OnEnd;

        private IBoard board;
        private int turn = 0;
        private int currentPlayer;

        public IPlayer[] Players { get; private set; }
        public IMatchRequestHandler LocalPlayer => Players.OfType<IMatchRequestHandler>().FirstOrDefault();
        private IPlayer CurrentPlayer => Players[currentPlayer];

        public Match(IBoard boardSetup, IPlayer localPlayer, IPlayer remotePlayer)
        {
            board = boardSetup;
            Players = new IPlayer[2] { localPlayer, remotePlayer };
        }

        public void Launch()
        {
            turn = 0;
            OnSwitchTurn?.Invoke(CurrentPlayer);
            Debug.Log($"[Core/GameMatch] - Start");
        }

        public void RequestMovement(IPlayer player, IPiece piece, Vector2Int movement)
        {
            if (player.Id != CurrentPlayer.Id || !piece.GetValidMoves(board).Contains(movement))
                return;

            board.Locate(piece, movement);
            Debug.Log($"[Core/Match] - {player.Name} Move {piece.Id} to {movement}");

            if (!TatedrezUtils.CheckVictory(board, piece))
            {
                turn++;
                int next = (currentPlayer + 1) % 2;
                currentPlayer = Players[next].HasAvailableMoves(board) ? next : currentPlayer;
                OnSwitchTurn?.Invoke(CurrentPlayer);
                Debug.Log($"[Core/Match] - Launch turn {turn} - {CurrentPlayer.Name}");
                return;
            }

            OnEnd?.Invoke(player);
            Debug.Log($"[Core/Match] - End - {player.Name} wins!");
        }
    }
}