using UnityEngine;

namespace Utils
{
    public class ExitApplicationOnKeyCodePressed : MonoBehaviour
    {
        [SerializeField]
        private KeyCode keyCode = KeyCode.Escape;

        private void Update()
        {
            if (IsKeyCodePressed(keyCode)) ExitApplication();
        }

        private bool IsKeyCodePressed(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }

        private void ExitApplication()
        {
            Application.Quit();
        }
    }
}