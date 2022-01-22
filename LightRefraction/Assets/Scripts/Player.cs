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
        public Interactable mountingInteractableObj { get; set; }
        void Update()
        {
            //movement / handle input
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            //interact
            if(Input.GetKeyDown(KeyCode.Z))
            {
                if(mountingInteractableObj != null)
                {
                    mountingInteractableObj.Interact(); //dismount
                    mountingInteractableObj = null;
                }
                else
                {
                    Interactable newInteractable = interactionArea.Interactable;
                    if (newInteractable != null)
                    {
                        newInteractable.Interact();
                        if (newInteractable.isMountable)
                            mountingInteractableObj = newInteractable;
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (mountingInteractableObj == null)
            {
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
        public void Dead()
        {
            if(mountingInteractableObj != null)
            {
                mountingInteractableObj.Interact(); //dismount
                mountingInteractableObj = null;
            }
            transform.position = GameManager.Instance.GeneratedStage.startPosition.position;
        }
    }

}