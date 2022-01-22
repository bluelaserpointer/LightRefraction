using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInteractionArea : MonoBehaviour
    {
        public Interactable Interactable => interactables.Count > 0 ? interactables[0] : null;
        List<Interactable> interactables = new List<Interactable>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactables.Add(interactable);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactables.Remove(interactable);
            }
        }
    }
}