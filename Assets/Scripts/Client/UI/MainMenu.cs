using System;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class MainMenu : Screen
    {
        [SerializeField] private Button startButton;
        private Action startMatchAction;

        protected override void OnDisplay(params object[] parameters)
        {
            startMatchAction = parameters[0] as Action;
            startButton.onClick.AddListener(OnClickStartButton);
        }

        private void OnClickStartButton()
        {
            startMatchAction?.Invoke();
        }

        protected override void OnClose()
        {
            startButton.onClick.RemoveListener(OnClickStartButton);
            startMatchAction = null;
        }
    }
}