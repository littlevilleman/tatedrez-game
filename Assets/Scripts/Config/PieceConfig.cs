using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PieceConfig", menuName = "Tatedrez/Config/Piece", order = 1)]
    public class PieceConfig : ScriptableObject
    {
        public EPiece piece;
        public Sprite sprite;
    }

    public enum EPiece
    {
        KNIGHT, ROOK, BISHOP
    }
}