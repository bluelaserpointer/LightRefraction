using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Handle : MonoBehaviour
    {
        Interactable interactable;
        void Awake()
        {
            interactable = GetComponent<Interactable>();
            interactable.OnInteract.AddListener(OnInteract);
        }
        private void OnInteract()
        {

        }
        void Update()
        {

        }
    }

}