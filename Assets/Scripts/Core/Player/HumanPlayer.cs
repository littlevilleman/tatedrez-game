using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class HumanPlayer : Player, IPlayer
    {
        public List<IPiece> PendingPieces => Pieces.Where(x => !x.IsLocated).ToList();

        public HumanPlayer(int id, string name, IPiece[] pieceSet)
        {
            Id = id;
            Name = name;
            Pieces = pieceSet;
        }

        public override void OpenTurn(ITurnDispatcher turnDispatcher, IBoard board)
        {
            if (!HasAvailableMoves(board))
                CloseTurn(turnDispatcher);
        }

        public override void CloseTurn(ITurnDispatcher turnDispatcher)
        {
            turnDispatcher.DispatchTurn(this);
        }
    }
}