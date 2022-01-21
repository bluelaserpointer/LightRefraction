using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class DirectionalLightSource : MonoBehaviour
    {
        [SerializeField]
        [Min(0f)]
        float raycastDistance = 100;

        LightLine firstLightLine;
        private void Start()
        {
            UpdateLight();
        }
        public void UpdateLight()
        {
            //TODO: improve performance
            //EmitDirectionalLight(transform.position, transform.right);
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }
    }
}