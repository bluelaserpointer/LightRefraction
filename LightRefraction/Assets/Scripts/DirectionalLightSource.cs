using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class DirectionalLightSource : MonoBehaviour
    {
        public void Update()
        {
            GameManager.Instance.EmitLightLine(transform.position, transform.right);
        }
    }
}