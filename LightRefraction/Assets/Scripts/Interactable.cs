using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class Interactable : MonoBehaviour
    {
        public UnityEvent OnInteract;
    }
}