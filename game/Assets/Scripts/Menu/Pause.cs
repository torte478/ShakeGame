using UnityEngine;
using UnityEngine.InputSystem;

namespace Shake.Menu
{
    internal sealed class Pause : MonoBehaviour
    {
        private InputAction _input;

        [SerializeField]
        private GameObject ui;

        public bool Paused { get; private set; }
        
        public static Pause Instance { get; private set; }

        void Awake()
        {
            Instance = this;

            _input = new Controls().Menu.Pause;
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.performed += TogglePause;
        }

        void OnDisable()
        {
            _input.Disable();
            _input.performed -= TogglePause;
        }

        private void TogglePause(InputAction.CallbackContext context)
        {
            if (Paused)
            {
                ui.SetActive(false);
                Time.timeScale = 1f;
                Paused = false;
            }
            else
            {
                ui.SetActive(true);
                Time.timeScale = 0f;
                Paused = true;
            }
        }
    }
}