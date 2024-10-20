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
            ICollection<Vector2Int> checkDirections = IsOwningCenter(board, piece.OwnerId) ? Directions : OrthogonalDirections;
            foreach (Vector2Int dir in checkDirections)
                for (int i = 1; i <= 2; i++)
                {
                    Vector2Int location = ModulateLocation(piece.Location + dir * i, board.Size);
                    ILocatable neighbour = board.GetLocatable(location);
                    if (neighbour?.OwnerId != piece.OwnerId)
                        break;

                    if (i == 2)
                        return true;
                }

            return false;
        }

        private static bool IsOwningCenter(IBoard board, int playerId)
        {
            return board.GetLocatable(Vector2Int.one)?.OwnerId == playerId;
        }

        public static bool IsValidSelection(IPlayer player, IPiece piece)
        {
            return piece != null && player.Id == piece.OwnerId && (!piece.IsLocated || player.PendingPieces.Length == 0);
        }

        private static Vector2Int ModulateLocation(Vector2Int location, int mod)
        {
            return new Vector2Int((location.x % mod + mod) % mod, (location.y % mod + mod) % mod);
        }
    }
}