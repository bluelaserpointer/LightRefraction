using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {
        public UnityEvent OnInteract;
        public void Interact()
        {
            OnInteract.Invoke();
        }
    }
}