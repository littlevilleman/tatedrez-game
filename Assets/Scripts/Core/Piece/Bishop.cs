using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Pieces
{
    public class Bishop: IPieceStrategy
    {
        public Vector2Int[] GetValidMoves(IBoard board, Vector2Int location)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            foreach (Vector2Int direction in TatedrezUtils.DiagonalDirections)
            {
                for (int i = 1; i < board.Size; i++)
                {
                    if (board.GetLocatable(location + direction * i) != null)
                        break;

                    moves.Add(location + direction * i);
                }
            }

            return moves.Where(x => board.IsInsideBounds(x)).ToArray();
        }
    }
}
