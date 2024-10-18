using Client.UI;
using Core;
using System;
using UnityEngine;

namespace Client
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ConfigManager config;
        [SerializeField] private UIManager ui;
        [SerializeField] private InputManager input;
        [SerializeField] private MatchManager matchManager;

        private IMatchBuilder matchBuilder;

        private void Start()
        {
            matchBuilder = new MatchBuilder();
            Action a = () => matchManager.RequestStartMatch();

            ui.DisplayScreen<MainMenu>(a);
            matchManager.Setup(matchBuilder, config, ui, input);
        }
    }
}