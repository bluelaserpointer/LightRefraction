using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.attachedRigidbody == GameManager.Player.Rigidbody)
            {
                GameManager.Instance.NextStage();
            }
        }
    }
}