using Client.Input;
using Client.UI;
using UnityEngine;

namespace Client
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ConfigManager config;
        [SerializeField] private UIManager ui;
        [SerializeField] private InputManager input;
        [SerializeField] private MatchManager matchManager;

        private void Start()
        {
            matchManager.Setup(config, ui, input);
            ui.DisplayScreen<MainMenuScreen>(matchManager);
        }
    }
}