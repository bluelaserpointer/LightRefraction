using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LineRenderer))]
    public class LightLine : MonoBehaviour
    {
        [SerializeField]
        LineRenderer _lineRenderer;
        [SerializeField]
        [Min(0)]
        float lifeTime = 0.1F;

        IzumiLib.Timestamp awakeTimeStamp = new IzumiLib.Timestamp();
        public LineRenderer LineRenderer => _lineRenderer;

        private void Awake()
        {
            awakeTimeStamp.Stamp();
        }
        public void Init(Vector2 start, Vector2 end)
        {
            _lineRenderer.SetPositions(new Vector3[] {start, end});
        }
        private void Update()
        {
            if(awakeTimeStamp.PassedTime > lifeTime)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}