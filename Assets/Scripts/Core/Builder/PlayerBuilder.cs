using Config;

namespace Core.Builder
{
    public class PlayerBuilder
    {
        public IPlayer Build(int id, PlayerConfig config, IPiece[] pieceSet)
        {
            if (config.isAI)
                return new AIPlayer(id, config.name, pieceSet);

            return new HumanPlayer(id, config.name, pieceSet);
        }
    }
}