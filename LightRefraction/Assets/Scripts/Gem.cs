using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Gem : MonoBehaviour
    {
        [SerializeField]
        List<float> degreeAngles;

        [Header("RefractionIndcate")]
        [SerializeField]
        GameObject refractDirectionIndicator;

        public List<float> DegreeAngles => degreeAngles;

        private void Start()
        {
            foreach (float angle in DegreeAngles)
            {
                Instantiate(refractDirectionIndicator, transform).transform.eulerAngles
                        = Vector3.forward * angle;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach (float angle in DegreeAngles)
            {
                float radian = angle * Mathf.Deg2Rad;
                float length = 1;
                Gizmos.DrawLine(transform.position, transform.position + new Vector3(length * Mathf.Cos(radian), length * Mathf.Sin(radian), 0));
            }
        }
    }
}