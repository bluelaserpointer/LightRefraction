using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LightBlocker))]
    public class LightenColorChanger : MonoBehaviour
    {
        [SerializeField]
        Color unlitColor, litColor;
        [SerializeField]
        List<SpriteRenderer> renderers;
        [SerializeField]
        [Min(0)]
        float colorChangeScale = 1F;

        LightBlocker _lightBlocker;
        private void Awake()
        {
            _lightBlocker = GetComponent<LightBlocker>();
        }
        void Update()
        {
            Color color = Color.LerpUnclamped(litColor, unlitColor, Mathf.Clamp01(_lightBlocker.PassedTimeFromLastLighten * colorChangeScale));
            renderers.ForEach(r => r.color = color);
        }
    }

}