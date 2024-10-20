using Config;
using System.Collections.Generic;
using System.Linq;

namespace Core.Builder
{
    public interface IMatchBuilder
    {
        public event BuildMatch OnBuild;
        public IMatch Build(IGameConfig config);
    }

    public class MatchBuilder : IMatchBuilder
    {
        public event BuildMatch OnBuild;
        private PieceBuilder pieceBuilder = new PieceBuilder();
        private PlayerBuilder playerBuilder = new PlayerBuilder();

        public IMatch Build(IGameConfig config)
        {
            IBoard board = BuildBoard(config);
            IPlayer[] players = BuildPlayers(config);
            IMatch match = new Match(board, players[0], players[1]);
            
            OnBuild?.Invoke(match, board);
            return match;
        }

        private IBoard BuildBoard(IGameConfig config)
        {
            return new Board(config.Match.Board.size);
        }

        private IPlayer[] BuildPlayers(IGameConfig config)
        {
            IPlayer[] players = new IPlayer[2];

            for (int i = 0; i < 2; i++)
            {
                IPiece[] playerPieces = BuildPieceSet(config, i);
                players[i] = playerBuilder.Build(i, config.Players[i], playerPieces);
            }

            return players;
        }

        public IPiece[] BuildPieceSet(IGameConfig config, int playerId)
        {
            ICollection<IPiece> pieces = new List<IPiece>();
            foreach (EPieceId pieceId in config.Match.PieceSet)
                pieces.Add(pieceBuilder.Build(playerId, config.GetPieceConfig(pieceId)));

            return pieces.ToArray();
        }
    }
}