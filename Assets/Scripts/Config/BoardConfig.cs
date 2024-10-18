using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Tatedrez/Config/Board", order = 1)]
    public class BoardConfig : ScriptableObject
    {
        [field: SerializeField][field: Range(3, 5)] public int size { get; private set; } = 3;
    }
}