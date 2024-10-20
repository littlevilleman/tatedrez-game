using System;
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

        public IPlayer[] Players { get; private set; }
        public IMatchRequestHandler LocalPlayer => Players.OfType<IMatchRequestHandler>().FirstOrDefault();

        private IBoard board;
        private int turn = 0;
        private int currentPlayer;

        public Match(IBoard boardSetup, IPlayer localPlayer, IPlayer remotePlayer)
        {
            board = boardSetup;
            Players = new IPlayer[2] { localPlayer, remotePlayer };
        }

        public void Launch()
        {
            turn = 0;
            OnSwitchTurn?.Invoke(Players[currentPlayer]);
            Debug.Log($"[Core/GameMatch] - Start");
        }

        public void RequestMovement(IPlayer player, IPiece piece, Vector2Int movement)
        {
            if (player.Id != Players[currentPlayer].Id || !piece.GetValidMoves(board).Contains(movement))
                return;
            
            Debug.Log($"[Core/Match] - {player.Name} Request move {piece.Id} to {movement}");


            board.Locate(piece, movement);

            if (piece != null && TatedrezUtils.CheckVictory(board, piece))
            {
                OnEnd?.Invoke(player);
                Debug.Log($"[Core/Match] - End - {player.Name} wins!");
                return;
            }

            bool skip = CheckSkip(currentPlayer);
            turn++;
            currentPlayer = (player.Id + (skip ? 2 : 1)) % 2;
            Debug.Log($"[Core/Match] - Launch turn {turn} - {Players[currentPlayer].Name}");
            OnSwitchTurn?.Invoke(Players[currentPlayer]);
        }

        private bool CheckSkip(int playerId) => !Players[(playerId + 1) % 2].HasAvailableMoves(board);
    }
}