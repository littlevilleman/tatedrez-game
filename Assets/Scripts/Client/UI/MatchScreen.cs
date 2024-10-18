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
            ConfigManager config = parameters[1] as ConfigManager;
            match.OnStepTurn += OnStepTurn;

            localPlayerWidget.Setup(config.GetPlayerConfig(0));
            remotePlayerWidget.Setup(config.GetPlayerConfig(1));
        }

        private void OnStepTurn(IPlayer currentPlayer)
        {
            localPlayerWidget.Highlight(currentPlayer.Id == 0);
            remotePlayerWidget.Highlight(currentPlayer.Id == 1);
        }

        protected override void OnClose()
        {
        }
    }
}