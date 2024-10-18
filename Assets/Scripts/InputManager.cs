using UnityEngine;

namespace Client
{
    public class InputManager : MonoBehaviour
    {
        private TouchControls touchControls;

        private void Awake()
        {
            touchControls = new TouchControls();
        }

        public void Setup(BoardBehaviour boardBehaviour)
        {
#if UNITY_EDITOR
            touchControls.Touch.Click.performed += boardBehaviour.OnClick;
#else
            touchControls.Touch.TouchPress.started += boardBehaviour.OnClick;
#endif
            touchControls.Enable();
        }

        public Vector2 GetClickPosition(Camera cam)
        {
#if UNITY_EDITOR
            return cam.ScreenToWorldPoint(touchControls.Touch.ClickPosition.ReadValue<Vector2>());
#else
            return cam.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
#endif
        }

        public void Disable()
        {
            touchControls.Disable();
        }
    }
}