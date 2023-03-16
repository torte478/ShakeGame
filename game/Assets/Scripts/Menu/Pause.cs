using System;
using UnityEngine;

namespace Shake.Menu
{
    internal sealed class Pause : MonoBehaviour
    {
        [SerializeField]
        private GameObject ui;
        
        public bool Paused { get; private set; }
        
        public static Pause Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            TogglePause();
        }

        private void TogglePause()
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