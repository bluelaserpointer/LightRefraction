using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class OnEnableEvent : MonoBehaviour
    {
        public UnityEvent onEnable, onDisable;
        private void OnEnable()
        {
            onEnable.Invoke();
        }
        private void OnDisable()
        {
            onDisable.Invoke();
        }
    }
}