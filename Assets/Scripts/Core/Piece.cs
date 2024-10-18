using Config;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public enum EMoveResult
    {
        FAILED, LOCATE, MOVE
    }

    public interface IPiece
    {
        public event LocatePiece OnLocate;
        public int OwnerId { get; }
        public bool IsLocated { get; }
        public Vector2Int Location { get; }
        public EMoveResult TryMove(IBoard board, Vector2Int location);
        public List<Vector2Int> GetValidMoves(IBoard board);
    }

    public abstract class Piece : IPiece
    {
        public int OwnerId { get; private set; }
        public bool IsLocated { get; private set; }
        public Vector2Int Location { get; private set; } = -Vector2Int.one;

        public event LocatePiece OnLocate;

        public abstract List<Vector2Int> GetValidMoves(IBoard board);

        protected Piece(int ownerIdSetup)
        {
            OwnerId = ownerIdSetup;
        }

        public EMoveResult TryMove(IBoard board, Vector2Int location)
        {
            if (!IsLocated && !board.IsValidLocation(location))
                return EMoveResult.FAILED;

            if (IsLocated && !GetValidMoves(board).Contains(location))
                return EMoveResult.FAILED;

            EMoveResult result = IsLocated ? EMoveResult.MOVE : EMoveResult.LOCATE;
            Location = location;
            IsLocated = true;
            OnLocate?.Invoke(this, result);
            return result;
        }
    }
}