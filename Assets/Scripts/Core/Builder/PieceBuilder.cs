using Config;
using Core.Pieces;

namespace Core.Builder
{
    public class PieceBuilder
    {
        public IPiece Build(int playerId, PieceConfig config)
        {
            return new Piece(config.Id, playerId, BuildStrategy(config.Id));
        }

        private IPieceStrategy BuildStrategy(EPieceId pieceId)
        {
            switch (pieceId)
            {
                case EPieceId.KNIGHT: return new Knight();
                case EPieceId.ROOK: return new Rook();
                case EPieceId.BISHOP: return new Bishop();
            }

            return null;
        }
    }
}