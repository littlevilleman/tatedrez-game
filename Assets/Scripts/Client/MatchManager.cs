using Client.Input;
using Client.UI;
using Config;
using Core;
using Core.Builder;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class MatchManager : MonoBehaviour
    {
        [SerializeField] private PiecePool piecePool;
        [SerializeField] private BoardBehaviour boardBhv;

        private UIManager ui;
        private IGameConfig config;
        private IMatchBuilder matchBuilder = new MatchBuilder();
        private IMatchInputObserver inputHandler;
        private IMatch match;

        public void Setup(IGameConfig configSetup, UIManager uiSetup, InputManager input)
        {
            config = configSetup;
            ui = uiSetup;

            matchBuilder.OnBuild += (match, board) => OnBuildMatch(match, board, input);
        }

        public void RequestStartMatch()
        {
            IMatch match = matchBuilder.Build(config);
            ui.DisplayScreen<MatchScreen>(match, config);
        }

        private void OnBuildMatch(IMatch matchSetup, IBoard board, InputManager input)
        {
            match = matchSetup;
            match.OnSwitchTurn += (player) => OnSwitchTurn(player, board);
            match.OnEnd += OnEndMatch;

            inputHandler = new MatchInputHandler(match, boardBhv, input);
            boardBhv.Setup(board, match.Players, piecePool, config);

            match.Launch();
        }

        private void OnSwitchTurn(IPlayer currentPlayer, IBoard board)
        {
            StartCoroutine(OpenTurnDelayed(currentPlayer, board, .25f));
        }

        private IEnumerator OpenTurnDelayed(IPlayer player, IBoard board, float delay)
        {
            yield return new WaitForSeconds(delay);
            player.OpenTurn(match, board);
        }

        private void OnEndMatch(IPlayer victoryPlayer)
        {
            match.OnEnd -= OnEndMatch;
            StartCoroutine(CloseDelayed(victoryPlayer, .5f));
        }

        private IEnumerator CloseDelayed(IPlayer victoryPlayer, float delay)
        {
            yield return new WaitForSeconds(delay);

            MatchResultPopupContext resultContext = new MatchResultPopupContext { confirmCallback = OnMatchResultConfirm, victoryPlayer = victoryPlayer };
            ui.GetPopup<MatchResultPopupContext>().Display(resultContext);
        }

        private void OnMatchResultConfirm()
        {
            boardBhv.Recycle(piecePool);
            ui.DisplayScreen<MainMenuScreen>(this);
        }
    }
}