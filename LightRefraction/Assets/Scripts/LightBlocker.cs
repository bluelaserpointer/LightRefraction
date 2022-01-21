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

        public UnityEvent<LightLine> OnLighten;
    }
}