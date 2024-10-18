using System.Collections.Generic;
using UnityEngine;

namespace Core.Pieces
{
    public class Knight : Piece, IPiece
    {
        public Knight(int ownerIdSetup) : base(ownerIdSetup)
        {
        }

        public override List<Vector2Int> GetValidMoves(IBoard board)
        {
            List<Vector2Int> moves = new List<Vector2Int>();

            foreach (Vector2Int direction in TatedrezUtils.OrthogonalDirections)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Vector2Int location = Location + direction * 2 + new Vector2Int(direction.y, direction.x) * i;

                    if (board.IsValidLocation(location))
                        moves.Add(location);
                }
            }

            return moves;
        }
    }
}
