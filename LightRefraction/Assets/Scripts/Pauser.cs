using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class Pauser : MonoBehaviour
    {
        float _originalTimeScale;
        public bool Paused { get; private set; }
        public void Pause(bool pause)
        {
            if (pause)
                Pause();
            else
                Unpause();
        }
        public void Pause()
        {
            if (Paused)
                return;
            Paused = true;
            _originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        public void Unpause()
        {
            if (!Paused)
                return;
            Paused = false;
            Time.timeScale = _originalTimeScale;
        }
    }
}
