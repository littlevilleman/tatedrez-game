namespace Core
{
    public interface IMatchBuilder
    {
        public event BuildMatch OnBuild;
        public IMatch Build(ConfigManager config);
    }

    public class MatchBuilder : IMatchBuilder
    {
        public event BuildMatch OnBuild;

        public IMatch Build(ConfigManager config)
        {
            IBoard board = BuildBoard(config);
            IPlayer[] players = BuildPlayers(config);
            IMatch match = new Match(board, players[0], players[1]);
            
            OnBuild?.Invoke(match);

            return match;
        }

        private IBoard BuildBoard(ConfigManager config)
        {
            int size = config.GetBoardConfig().size;
            return new Board(size);
        }

        private IPlayer[] BuildPlayers(ConfigManager config)
        {
            IPlayer[] players = new IPlayer[2];

            for (int i = 0; i < 2; i++)
                players[i] = config.GetPlayerConfig(i).Build(i, config.GetPieceSet(i));

            return players;
        }
    }
}