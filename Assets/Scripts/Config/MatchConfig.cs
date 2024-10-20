using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    public interface IGameConfig
    {
        public List<PlayerConfig> Players { get; }
        public MatchConfig Match { get; }

        public PieceConfig GetPieceConfig(EPieceId id);
    }

    [CreateAssetMenu(fileName = "MatchConfig", menuName = "Tatedrez/Config/Match", order = 1)]
    public class MatchConfig : ScriptableObject
    {
        [field : SerializeField] public BoardConfig Board { get; private set; }
        [field : SerializeField] public List<EPieceId> PieceSet { get; private set; }
    }
}