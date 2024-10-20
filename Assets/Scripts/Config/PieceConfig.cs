using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "PieceConfig", menuName = "Tatedrez/Config/Piece", order = 1)]
    public class PieceConfig : ScriptableObject
    {
        [field:SerializeField] public EPieceId Id { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }

    public enum EPieceId
    {
        KNIGHT, ROOK, BISHOP
    } 
}