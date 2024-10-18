using Client;
using Client.UI;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private PiecePool piecePool;
    [SerializeField] private BoardBehaviour boardBhv;
    [SerializeField] private Transform[] playerPiecesHolders;
    public List<PieceBehaviour> PiecesBhv { get; private set; } = new List<PieceBehaviour>();

    private IMatchBuilder matchBuilder;
    private ConfigManager config;
    private UIManager ui;
    private IMatch match;

    public void Setup(IMatchBuilder matchBuilderSetup, ConfigManager configSetup, UIManager uiSetup, InputManager input)
    {
        config = configSetup;
        ui = uiSetup;

        matchBuilder = matchBuilderSetup;
        matchBuilder.OnBuild += (match) => OnBuildMatch(match, input);
    }

    public void RequestStartMatch()
    {
        IMatch match = matchBuilder.Build(config);
        ui.DisplayScreen<MatchScreen>(match, config);
    }

    private void OnBuildMatch(IMatch matchSetup, InputManager input)
    {
        match = matchSetup;
        match.OnEnd += OnMatchEnd;

        boardBhv.Setup(match, input);

        for (int iPlayer = 0; iPlayer < match.Players.Length; iPlayer++)
        {
            for (int iPiece = 0; iPiece < match.Players[iPlayer].Pieces.Length; iPiece++)
            {
                IPiece piece = match.Players[iPlayer].Pieces[iPiece];
                PieceBehaviour pieceBhv = piecePool.Pull(playerPiecesHolders[iPlayer]);
                pieceBhv.Setup(piece, iPiece, config.GetPieceConfig(piece));
                PiecesBhv.Add(pieceBhv);
            }
        }

        match.Launch();
    }

    private void OnMatchEnd(IPlayer victoryPlayer)
    {
        StartCoroutine(CloseDelayed(victoryPlayer, .5f));
    }

    private IEnumerator CloseDelayed(IPlayer victoryPlayer, float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseMatch(victoryPlayer);
    }

    private void CloseMatch(IPlayer victoryPlayer)
    {
        match.OnEnd -= OnMatchEnd;

        Action confirmCallback = OnResultConfirm;
        ui.GetPopup<MatchResultPopupContext>().Display(new MatchResultPopupContext { confirmCallback = confirmCallback, victoryPlayer = victoryPlayer });
    }

    private void OnResultConfirm()
    {
        foreach (PieceBehaviour pieceBhv in PiecesBhv)
            pieceBhv.Recycle(piecePool);

        Action a = () => RequestStartMatch();
        ui.DisplayScreen<MainMenu>(a);
    }
}