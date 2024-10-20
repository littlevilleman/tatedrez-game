using UnityEngine;

namespace Client.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        private TouchControls touchControls;

        private void Awake()
        {
            touchControls = new TouchControls();
        }

        public void OnEnable()
        {
            touchControls.Enable();
        }

        public void AddObserver(IMatchInputObserver observer)
        {
#if UNITY_EDITOR
            touchControls.Touch.TouchPress.started += observer.OnClick;
#else
            touchControls.Touch.TouchPress.started += observer.OnClick;
#endif
        }

        public Vector2 GetClickPosition()
        {
#if UNITY_EDITOR
            return cam.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
#else
            return cam.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
#endif
        }

        public void RemoveObserver(IMatchInputObserver observer)
        {
#if UNITY_EDITOR
            touchControls.Touch.TouchPress.started -= observer.OnClick;
#else
            touchControls.Touch.TouchPress.started -= observer.OnClick;
#endif
        }

        public void OnDisable()
        {
            touchControls.Disable();
        }
    }
}