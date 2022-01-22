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

        Gem _holdingGem;
        public Gem HoldingGem
        {
            get => _holdingGem;
            set
            {
                _holdingGem = value;
                GameManager.Instance.UpdateGemUI();
            }
        }

        //data
        public Vector3 input;
        [HideInInspector]
        public bool preventMove;
        public Interactable interactingObj;
        void Update()
        {
            //movement / handle input
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            //interact
            if(Input.GetKeyDown(KeyCode.Z))
            {
                print("Z");
                if(interactingObj != null && interactionArea.InArea(interactingObj))
                {
                    print("interact old");
                    interactingObj.Interact();
                }
                else if(interactionArea.Interactable != null)
                {
                    (interactingObj = interactionArea.Interactable).Interact();
                    print("interact new: " + interactingObj);
                }
            }
        }
        private void FixedUpdate()
        {
            if (!preventMove)
            {
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

}