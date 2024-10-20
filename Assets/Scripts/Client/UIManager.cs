using System.Collections.Generic;
using UnityEngine;

namespace Client.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        public static UIManager Instance => instance;

        [SerializeField] private List<Screen> screens;
        [SerializeField] private List<PopupBase> popups;
        [SerializeField] private Canvas screenLayer;
        [SerializeField] private Canvas popupLayer;

        private IScreen currentScreen;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            foreach (Screen screen in screens)
                screen.gameObject.SetActive(false);
        }

        public IScreen GetScreen<T>() where T : IScreen
        {
            foreach (IScreen screen in screens)
            {
                if (screen.GetType() == typeof(T))
                    return screen;
            }

            Debug.LogWarning($"Screen {typeof(T)} not found");
            return null;
        }

        public Popup<T> GetPopup<T>() where T : PopupContext
        {
            foreach (Popup<T> popup in popups)
            {
                return popup;
            }

            Debug.LogWarning($"Popup {typeof(T)} not found");
            return null;
        }

        public void DisplayScreen<T>(params object[] parameters) where T : IScreen
        {
            currentScreen?.Close();
            currentScreen = GetScreen<T>();
            currentScreen?.Display(parameters);
        }
    }
}

