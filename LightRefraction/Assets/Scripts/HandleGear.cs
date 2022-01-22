using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class HandleGear : MonoBehaviour
    {
        public Vector2 positionMove;
        public float zRotate;

        float _handleAmount;
        Rigidbody2D _rigidbody2D;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void SetHandleAmount(float amount)
        {
            _handleAmount = amount;
        }
        private void FixedUpdate()
        {
            if(_handleAmount != 0)
            {
                if(_rigidbody2D != null)
                    _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                float factor = _handleAmount * Time.fixedDeltaTime;
                transform.Translate(factor * positionMove);
                transform.eulerAngles += Vector3.forward * factor * zRotate;
            }
            else if (_rigidbody2D != null)
            {
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}