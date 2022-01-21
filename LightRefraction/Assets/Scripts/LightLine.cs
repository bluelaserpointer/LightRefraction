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
        float _maxDistance = 100;

        List<LightLine> branches = new List<LightLine>();
        public LineRenderer LineRenderer => _lineRenderer;
        public Vector2 Origin { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 End { get; private set; }
        public float Length => Vector2.Distance(Origin, End);

        private void UpdateLine()
        {
            foreach(LightLine eachLightLine in branches)
            {
                eachLightLine.gameObject.SetActive(false);
            }
            bool foundEnd = false;
            foreach (RaycastHit2D hitInfo in Physics2D.RaycastAll(Origin, Direction, _maxDistance))
            {
                LightBlocker lightBlocker = hitInfo.collider.gameObject.GetComponent<LightBlocker>();
                if (lightBlocker != null)
                {
                    if (lightBlocker.doBlock)
                    {
                        End = hitInfo.point;
                        foundEnd = true;
                        lightBlocker.OnLighten.Invoke(this);
                        break;
                    }
                }
            }
            if(!foundEnd)
                End = Origin + Direction * _maxDistance;
            LineRenderer.SetPositions(new Vector3[] { Origin, End });
        }
        public LightLine AddBranch()
        {
            foreach(LightLine eachLightLine in branches)
            {
                if(!eachLightLine.gameObject.activeSelf)
                {
                    eachLightLine.gameObject.SetActive(true);
                    return eachLightLine;
                }
            }
            LightLine newLightLine = Instantiate(this, transform);
            branches.Add(newLightLine);
            return newLightLine;
        }
    }

}