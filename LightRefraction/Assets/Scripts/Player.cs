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
        Vector3 _input;
        [HideInInspector]
        public bool doMove;
        void Update()
        {
            //movement
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
            //interact
            if(Input.GetKeyDown(KeyCode.Z))
            {
                interactionArea.Interactable?.OnInteract.Invoke();
            }
        }
        private void FixedUpdate()
        {
            transform.Translate(_input * moveSpeed * Time.deltaTime);
        }
    }

}