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

        public UnityEvent<Vector2, Vector2, float> OnLighten;

        IzumiLib.Timestamp lightenTimestamp = new IzumiLib.Timestamp();
        public float PassedTimeFromLastLighten => lightenTimestamp.PassedTime;
        public bool IsLighten => PassedTimeFromLastLighten <= 0.1;
        public void Lighten(Vector2 position, Vector2 direction, float remainDistance)
        {
            OnLighten.Invoke(position, direction, remainDistance);
            lightenTimestamp.Stamp();
        }
    }
}