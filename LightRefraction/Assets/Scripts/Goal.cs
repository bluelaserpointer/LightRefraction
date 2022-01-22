using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.attachedRigidbody == GameManager.Player.Rigidbody)
            {
                GameManager.Instance.NextStage();
            }
        }
    }
}