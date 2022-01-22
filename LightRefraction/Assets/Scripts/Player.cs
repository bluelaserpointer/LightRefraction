using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Player : Unit
    {
        //inspector
        [SerializeField]
        float moveSpeed = 5F;
        [SerializeField]
        PlayerInteractionArea interactionArea;
        [SerializeField]
        Transform gemHolder;

        Gem _holdingGem;
        public Gem HoldingGem
        {
            get => _holdingGem;
            set
            {
                if((_holdingGem = value) != null)
                {
                    _holdingGem.transform.parent = gemHolder;
                    _holdingGem.transform.position = gemHolder.position;
                }
                GameManager.Instance.UpdateGemUI();
            }
        }

        //data
        public Vector3 input;
        public bool InteractionPreventMove { get; set; }
        public Interactable interactingObj;
        void Update()
        {
            //movement / handle input
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            //interact
            if(Input.GetKeyDown(KeyCode.Z))
            {
                if(interactingObj != null && interactionArea.InArea(interactingObj))
                {
                    interactingObj.Interact();
                }
                else if(interactionArea.Interactable != null)
                {
                    (interactingObj = interactionArea.Interactable).Interact();
                }
            }
        }
        private void FixedUpdate()
        {
            if (!InteractionPreventMove)
            {
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

}