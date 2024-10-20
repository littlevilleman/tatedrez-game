using System.Collections.Generic;
using UnityEngine;

namespace Core.Pieces
{
    public class Rook : IPieceStrategy
    {
        public ICollection<Vector2Int> GetValidMoves(IBoard board, Vector2Int location)
        {
            ICollection<Vector2Int> moves = new List<Vector2Int>();

            foreach(Vector2Int direction in TatedrezUtils.OrthogonalDirections)
            {
                for (int i = 1; i < board.Size; i++)
                {
                    if (board.GetLocatable(location + direction * i) !=null)
                        break;
                    
                    moves.Add(location + direction * i);
                }
            }

            return moves;
        }
    }
}