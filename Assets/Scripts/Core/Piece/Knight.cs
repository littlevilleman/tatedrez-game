using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Pieces
{
    public class Knight : IPieceStrategy
    {
        public Vector2Int[] GetValidMoves(IBoard board, Vector2Int location)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            foreach (Vector2Int direction in TatedrezUtils.OrthogonalDirections)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2Int perpendicular =  new Vector2Int(direction.y, direction.x) * i;
                    if (board.GetLocatable(location + direction * 2 + perpendicular) == null)
                        moves.Add(location + direction * 2 + perpendicular);
                }
            }

            return moves.Where(x=>board.IsInsideBounds(x)).ToArray();
        }
    }
}
