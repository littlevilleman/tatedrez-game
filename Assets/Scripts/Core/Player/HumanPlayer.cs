using UnityEngine;

namespace Core
{
    public interface IMatchRequestHandler : IPlayer
    {
        public void RequestMove(IMatch match, IPiece piece, Vector2Int location);
    }

    public class HumanPlayer : Player, IPlayer, IMatchRequestHandler
    {
        public HumanPlayer(int id, string name, IPiece[] pieceSet)
        {
            Id = id;
            Name = name;
            Pieces = pieceSet;
        }

        public override void OpenTurn(IMatch match, IBoard board)
        {
        }

        public void RequestMove(IMatch match, IPiece piece, Vector2Int move)
        {
            match.RequestMovement(this, piece, move);
        }
    }
}