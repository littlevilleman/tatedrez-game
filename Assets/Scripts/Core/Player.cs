using System.Linq;

namespace Core
{
    public interface IPlayer
    {
        public int Id { get; }
        public string Name { get; }
        public IPiece[] Pieces { get; }
        public IPiece[] PendingPieces { get; }
        public void OpenTurn(IMatch turnDispatcher, IBoard board);
        public bool HasAvailableMoves(IBoard board);
    }

    public abstract class Player : IPlayer
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public IPiece[] Pieces { get; protected set; }
        public IPiece[] PendingPieces => Pieces.Where(x => !x.IsLocated).ToArray();
        public abstract void OpenTurn(IMatch match, IBoard board);

        public bool HasAvailableMoves(IBoard board)
        {
            return Pieces.Any(x => x.GetValidMoves(board).Length > 0);
        }
    }
}