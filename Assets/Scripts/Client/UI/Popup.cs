using UnityEngine;

namespace Client.UI
{
    public abstract class Popup<T> : PopupBase where T : PopupContext
    {
        public abstract void Display(T context);

    }

    public abstract class PopupBase : MonoBehaviour
    {

    }
    public abstract class PopupContext
    {

    }
}