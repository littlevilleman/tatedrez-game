using Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public interface IPieceStrategy
    {
        public Vector2Int[] GetValidMoves(IBoard board, Vector2Int location);
    }

    public interface IPiece : ILocatable
    {
        public EPieceId Id { get; }
        public bool IsLocated { get; }
        public Vector2Int[] GetValidMoves(IBoard board);
    }

    public class Piece : IPiece
    {
        public event LocatePiece OnLocate;
        public EPieceId Id { get; private set; }
        public int OwnerId { get; private set; }
        public bool IsLocated { get; private set; }
        public Vector2Int Location { get; private set; } = -Vector2Int.one;

        protected IPieceStrategy strategy;

        public Piece(EPieceId idSetup, int ownerIdSetup, IPieceStrategy strategySetup)
        {
            Id = idSetup;
            OwnerId = ownerIdSetup;
            strategy = strategySetup;
        }

        public Vector2Int[] GetValidMoves(IBoard board)
        {
            if (IsLocated)
                return strategy.GetValidMoves(board, Location).ToArray();

            return TatedrezUtils.GetEmptyLocations(board);
        }

        public void SetLocation(Vector2Int location)
        {
            Location = location;
            IsLocated = true;
            OnLocate?.Invoke(Location);
        }
    }
}