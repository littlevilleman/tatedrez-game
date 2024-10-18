using UnityEngine;

namespace Client.UI
{
    public interface IScreen
    {
        public void Display(params object[] parameters);

        public void Close();
    }

    public abstract class Screen : MonoBehaviour, IScreen
    {
        protected abstract void OnDisplay(params object[] parameters);
        protected abstract void OnClose();

        public void Display(params object[] parameters)
        {
            gameObject.SetActive(true);
            OnDisplay(parameters);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            OnClose();
        }
    }
}