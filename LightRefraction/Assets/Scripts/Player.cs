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

        //data
        Vector3 _input;
        void Update()
        {
            //movement
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
        }
        private void FixedUpdate()
        {
            transform.Translate(_input * moveSpeed * Time.deltaTime);
        }
    }

}