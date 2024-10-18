using System.Collections.Generic;
using UnityEngine;

namespace Core.Pieces
{
    public class Rook : Piece, IPiece
    {
        public Rook(int ownerIdSetup) : base(ownerIdSetup)
        {
        }

        public override List<Vector2Int> GetValidMoves(IBoard board)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            foreach(Vector2Int direction in TatedrezUtils.OrthogonalDirections)
            {
                for (int i = 1; i < board.Size; i++)
                {
                    Vector2Int location = Location + direction * i;

                    if (!board.IsValidLocation(location))
                        break;
                    
                    moves.Add(location);
                }
            }

            return moves;
        }
    }
}