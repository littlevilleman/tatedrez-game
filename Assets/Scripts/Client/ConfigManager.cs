using Config;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class ConfigManager : MonoBehaviour, IGameConfig
    {
        [SerializeField] private List<PieceConfig> Pieces;

        [field: SerializeField] public List<PlayerConfig> Players { get; private set; }
        [field: SerializeField] public MatchConfig Match { get; private set; }

        private readonly Dictionary<EPieceId, PieceConfig> pieceDictionary = new Dictionary<EPieceId, PieceConfig>();

        private void Awake()
        {
            foreach (PieceConfig piece in Pieces)
                pieceDictionary.Add(piece.Id, piece);
        }

        public PieceConfig GetPieceConfig(EPieceId id)
        {
            if (!pieceDictionary.TryGetValue(id, out PieceConfig config))
                Debug.LogError($"Config not found for id: {id}");

            return config;
        }

        private void OnDestroy()
        {
            pieceDictionary.Clear();
        }
    }
}