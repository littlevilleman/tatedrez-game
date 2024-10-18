using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public static class TatedrezUtils
    {
        public static Vector2Int[] OrthogonalDirections = new Vector2Int[4]
        {
            Vector2Int.up, Vector2Int.right, -Vector2Int.up, -Vector2Int.right,
        };

        public static Vector2Int[] DiagonalDirections = new Vector2Int[4]
        {
            Vector2Int.up + Vector2Int.right, Vector2Int.up -Vector2Int.right, -Vector2Int.up + Vector2Int.right, -Vector2Int.up - Vector2Int.right
        };

        public static Vector2Int[] Directions = OrthogonalDirections.Concat(DiagonalDirections).ToArray();

        public static bool CheckPlayerVictory(IBoard board, IPlayer player)
        {
            Vector2Int pieceLocation = player.Pieces[0].Location;
            foreach (Vector2Int dir in Directions)
            {
                for (int i = 1; i < 3; i++)
                {
                    Vector2Int location = pieceLocation + dir * i;
                    board.TryGetPiece(location, out IPiece neighbour);
                    if (neighbour == null || neighbour.OwnerId != player.Id)
                        break;

                    if (i == 2)
                        return true;
                }
            }

            return false;
        }

        public static Vector2Int PositionToGridLocation(Vector3 position)
        {
            return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        }

        public static List<Vector2Int> GetValidLocations(IBoard board)
        {
            List<Vector2Int> randomLocations = new List<Vector2Int>();

            for (int x = 0; x < board.Size; x++)
                for (int y = 0; y < board.Size; y++)
                    if (board.TryGetPiece(new Vector2Int(x, y), out IPiece piece) && piece == null)
                        randomLocations.Add(new Vector2Int(x, y));

            return randomLocations;
        }

        public static Vector2Int GetRandomValidLocation(IBoard board)
        {
            List<Vector2Int> validLocations = GetValidLocations(board);
            return validLocations[Random.Range(0, validLocations.Count)];
        }
    }
}