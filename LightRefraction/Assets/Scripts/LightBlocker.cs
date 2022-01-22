using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class LightBlocker : MonoBehaviour
    {
        public bool doBlock = true;
        public bool doReflection = false;

        public UnityEvent<Vector2, Vector2> OnLighten;
    }
}