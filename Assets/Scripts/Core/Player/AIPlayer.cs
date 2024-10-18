using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class AIPlayer : Player, IPlayer
    {
        public List<IPiece> PendingPieces => Pieces.Where(x => !x.IsLocated).ToList();

        public AIPlayer(int id, string name, IPiece[] pieceSet)
        {
            Id = id;
            Name = name;
            Pieces = pieceSet;
        }

        public override void OpenTurn(ITurnDispatcher turnDispatcher, IBoard board)
        {
            if (PendingPieces.Count > 0)
                LocatePiece(board, turnDispatcher);
            else if(HasAvailableMoves(board))
                MovePiece(board, turnDispatcher);
            else
                CloseTurn(turnDispatcher);
        }

        private void MovePiece(IBoard board, ITurnDispatcher turnDispatcher)
        {
            IPiece piece = GetRandomValidPiece(board);
            List<Vector2Int> validLocations = piece.GetValidMoves(board).ToList();
            Vector2Int location = validLocations[Random.Range(0, validLocations.Count)];

            if (board.TryMovePiece(piece, location))
                CloseTurn(turnDispatcher);
        }

        private void LocatePiece(IBoard board, ITurnDispatcher turnDispatcher)
        {
            IPiece piece = PendingPieces[Random.Range(0, PendingPieces.Count)];
            Vector2Int location = TatedrezUtils.GetRandomValidLocation(board);

            if (board.TryMovePiece(piece, location))
                CloseTurn(turnDispatcher);
        }

        private IPiece GetRandomValidPiece(IBoard board)
        {
            List<IPiece> validPieces = Pieces.Where(x => x.GetValidMoves(board).Count > 0).ToList();
            return validPieces[Random.Range(0, validPieces.Count)];
        }

        public override void CloseTurn(ITurnDispatcher turnDispatcher)
        {
            turnDispatcher.DispatchTurn(this);
        }
    }
}