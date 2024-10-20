using UnityEngine;

namespace Core
{
    public interface IBoard
    {
        public int Size { get; }
        public ILocatable GetLocatable(Vector2Int location);
        public void Locate(IPiece piece, Vector2Int location);
        public bool IsInsideBounds(Vector2Int toLocation);
    }

    public interface ILocatable
    {
        public event LocatePiece OnLocate;
        public int OwnerId { get; }
        public Vector2Int Location { get; }
        public void SetLocation(Vector2Int location);
    }

    public class Board : IBoard
    {
        public int Size { get; private set; }
        private ILocatable[,] pieces;

        public Board(int sizeSetup)
        {
            Size = sizeSetup;
            pieces = new ILocatable[Size, Size];
        }

        public ILocatable GetLocatable(Vector2Int location)
        {
            if (IsInsideBounds(location))
                return pieces[location.x, location.y];

            return null;
        }

        private void SetLocatable(Vector2Int location, ILocatable locatable)
        {
            if (IsInsideBounds(location))
                pieces[location.x, location.y] = locatable;
        }

        public void Locate(IPiece piece, Vector2Int toLocation)
        {
            SetLocatable(piece.Location, null);
            piece.SetLocation(toLocation);
            SetLocatable(toLocation, piece);
        }

        public bool IsInsideBounds(Vector2Int toLocation)
        {
            return toLocation.x >= 0 && toLocation.x < Size && toLocation.y >= 0 && toLocation.y < Size;
        }
    }
}