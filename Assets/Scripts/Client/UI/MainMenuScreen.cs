using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class MainMenuScreen : Screen
    {
        [SerializeField] private Button startButton;
        private MatchManager matchManager;

        protected override void OnDisplay(params object[] parameters)
        {
            matchManager = parameters[0] as MatchManager;
            startButton.onClick.AddListener(OnClickStartButton);
        }

        private void OnClickStartButton()
        {
            matchManager.RequestStartMatch();
        }

        protected override void OnClose()
        {
            startButton.onClick.RemoveListener(OnClickStartButton);
        }
    }
}