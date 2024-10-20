using System.Linq;
using UnityEngine;

namespace Core
{
    public class AIPlayer : Player, IPlayer
    {
        public AIPlayer(int id, string name, IPiece[] pieceSet)
        {
            Id = id;
            Name = name;
            Pieces = pieceSet;
        }

        public override void OpenTurn(IMatch match, IBoard board)
        {
            IPiece piece = GetRandomPiece(board);
            Vector2Int move = GetRandomMove(board, piece);
            match.RequestMovement(this, piece, move);
        }

        private IPiece GetRandomPiece(IBoard board)
        {
            IPiece[] validPieces = PendingPieces.Length > 0 ? PendingPieces : Pieces.Where(x => x.GetValidMoves(board).Length > 0).ToArray();
            if (validPieces.Length > 0)
                return validPieces[Random.Range(0, validPieces.Length)];

            return null;
        }

        private Vector2Int GetRandomMove(IBoard board, IPiece piece)
        {
            Vector2Int[] validMoves = piece.GetValidMoves(board).ToArray();
            return validMoves[Random.Range(0, validMoves.Length)];
        }
    }
}