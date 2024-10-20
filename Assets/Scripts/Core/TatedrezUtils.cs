using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public static class TatedrezUtils
    {
        public static readonly ICollection<Vector2Int> OrthogonalDirections = new Vector2Int[4]
        {
            Vector2Int.up, Vector2Int.right, -Vector2Int.up, -Vector2Int.right,
        };

        public static readonly ICollection<Vector2Int> DiagonalDirections = new Vector2Int[4]
        {
            Vector2Int.up + Vector2Int.right, Vector2Int.up -Vector2Int.right, -Vector2Int.up + Vector2Int.right, -Vector2Int.up - Vector2Int.right
        };

        public static readonly ICollection<Vector2Int> Directions = OrthogonalDirections.Concat(DiagonalDirections).ToArray();


        public static Vector2Int[] GetEmptyLocations(IBoard board)
        {
            ICollection<Vector2Int> validLocations = new List<Vector2Int>();

            for (int x = 0; x < board.Size; x++)
                for (int y = 0; y < board.Size; y++)
                    if (board.GetLocatable(new Vector2Int(x, y)) == null)
                        validLocations.Add(new Vector2Int(x, y));

            return validLocations.ToArray();
        }

        public static Vector2Int PositionToGridLocation(Vector3 position)
        {
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        }

        public static bool CheckVictory(IBoard board, IPiece piece)
        {
            foreach (Vector2Int dir in Directions)
            {
                for (int i = 1; i <= 2; i++)
                {
                    Vector2Int location = ModulateLocation(piece.Location + dir * i, board.Size);
                    ILocatable neighbour = board.GetLocatable(location);
                    if (neighbour == null || neighbour.OwnerId != piece.OwnerId)
                        break;

                    if (i == 2)
                        return true;
                }
            }

            return false;
        }

        public static bool IsValidSelection(IPlayer player, IPiece piece)
        {
            if (piece == null || player.Id != piece.OwnerId || (piece.IsLocated && player.PendingPieces.Length > 0))
                return false;

            return true;
        }

        private static Vector2Int ModulateLocation(Vector2Int location, int mod)
        {
            return new Vector2Int(Modulate(location.x, mod), Modulate(location.y, mod));
        }

        private static int Modulate(int v, int mod)
        {
            return (v % mod + mod) % mod;
        }
    }
}