using Config;
using Core;
using UnityEngine;

namespace Client.UI
{
    public class MatchScreen : Screen
    {
        [SerializeField] private PlayerWidget localPlayerWidget;
        [SerializeField] private PlayerWidget remotePlayerWidget;

        protected override void OnDisplay(params object[] parameters)
        {
            IMatch match = parameters[0] as IMatch;
            IGameConfig config = parameters[1] as IGameConfig;
            match.OnSwitchTurn += OnSwitchTurn;

            localPlayerWidget.Setup(config.Players[0]);
            remotePlayerWidget.Setup(config.Players[1]);
        }

        private void OnSwitchTurn(IPlayer currentPlayer)
        {
            localPlayerWidget.Highlight(currentPlayer.Id == 0);
            remotePlayerWidget.Highlight(currentPlayer.Id == 1);
        }

        protected override void OnClose()
        {
        }
    }
}