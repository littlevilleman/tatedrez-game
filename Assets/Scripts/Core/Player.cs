using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public interface IPlayer
    {
        public int Id { get; }
        public string Name { get; }
        public IPiece[] Pieces { get; }
        public List<IPiece> PendingPieces { get; }

        public void OpenTurn(ITurnDispatcher turnDispatcher, IBoard board);
        public void CloseTurn(ITurnDispatcher turnDispatcher);
    }

    public abstract class Player
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public IPiece[] Pieces { get; protected set; }

        public abstract void OpenTurn(ITurnDispatcher turnDispatcher, IBoard board);
        public abstract void CloseTurn(ITurnDispatcher match);

        protected bool HasAvailableMoves(IBoard board)
        {
            return Pieces.Any(x => !x.IsLocated || x.GetValidMoves(board).Count > 0);
        }
    }
}