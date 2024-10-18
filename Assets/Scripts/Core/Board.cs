using System;
using UnityEngine;

namespace Core
{
    public interface IBoard
    {
        public int Size { get; }
        public bool TryGetPiece(Vector2Int location, out IPiece piece);
        public bool IsValidLocation(Vector2Int location);
        public bool TryMovePiece(IPiece piece, Vector2Int location);
    }

    public class Board : IBoard
    {
        public int Size { get; private set; }
        private IPiece[,] pieces;


        public Board(int sizeSetup)
        {
            Size = sizeSetup;
            pieces = new IPiece[Size, Size];
        }

        public bool IsValidLocation(Vector2Int location)
        {
            if (TryGetPiece(location, out IPiece piece) && piece == null)
                return true;

            return false;
        }

        public bool TryGetPiece(Vector2Int location, out IPiece piece)
        {
            try 
            { 
                piece = pieces[location.x, location.y]; 
            }
            catch (IndexOutOfRangeException)
            {
                piece = null;
                return false;
            }

            return true;
        }

        private bool TrySetPiece(Vector2Int location, IPiece piece)
        {
            try
            {
                pieces[location.x, location.y] = piece;
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool TryMovePiece(IPiece piece, Vector2Int location)
        {
            Vector2Int fromLocation = piece.Location;

            if (piece.TryMove(this, location) == EMoveResult.FAILED)
                return false;

            TrySetPiece(fromLocation, null);
            TrySetPiece(location, piece);
            Debug.Log($"[Core/Board] - Move piece {piece} {location}");
            return true;
        }
    }
}