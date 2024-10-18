using Config;
using Core;
using Core.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] private List<PieceConfig> pieceConfig;
    [SerializeField] private List<PlayerConfig> playerConfig;
    [SerializeField] private BoardConfig boardConfig;

    private readonly Dictionary<Type, PieceConfig> pieceDictionary = new Dictionary<Type, PieceConfig>();

    private void Awake()
    {
        pieceDictionary.Add(typeof(Knight), pieceConfig.First(x => x.piece == EPiece.KNIGHT));
        pieceDictionary.Add(typeof(Rook), pieceConfig.First(x => x.piece == EPiece.ROOK));
        pieceDictionary.Add(typeof(Bishop), pieceConfig.First(x => x.piece == EPiece.BISHOP));
    }

    public PieceConfig GetPieceConfig<T>(T piece) where T : IPiece
    {
        if (!pieceDictionary.TryGetValue(piece.GetType(), out PieceConfig config))
        {
            Debug.LogError($"Config not found for type: {typeof(T)}");
        }

        return config;
    }

    public IPiece[] GetPieceSet(int playerId)
    {
        return new IPiece[3] { new Knight(playerId), new Rook(playerId), new Bishop(playerId) };
    }

    public PlayerConfig GetPlayerConfig(int id)
    {
        return playerConfig[id];
    }

    public BoardConfig GetBoardConfig()
    {
        return boardConfig;
    }
}