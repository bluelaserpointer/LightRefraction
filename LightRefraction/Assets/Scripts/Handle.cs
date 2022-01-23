using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Handle : MonoBehaviour
    {
        [SerializeField]
        List<HandleGear> gears = new List<HandleGear>();

        [Header("Animation")]
        [SerializeField]
        float rotateSpeed = 5F;

        public bool BeingInteracted { get; private set; }
        Interactable _interactable;
        float _input;
        void Awake()
        {
            _interactable = GetComponent<Interactable>();
            _interactable.OnInteract.AddListener(OnInteract);
            _interactable.isMountable = true;
        }
        private void OnInteract()
        {
            if(BeingInteracted)
            {
                BeingInteracted = false;
                gears.ForEach(gear => gear.SetHandleAmount(0));
            }
            else
            {
                BeingInteracted = true;
            }
        }
        void Update()
        {
            if (!BeingInteracted)
                return;
            _input = GameManager.Player.input.x;
            gears.ForEach(gear => gear.SetHandleAmount(-_input));
        }
        private void FixedUpdate()
        {
            if(BeingInteracted && _input != 0)
                transform.eulerAngles += _input * Vector3.forward * rotateSpeed * Time.fixedDeltaTime;
        }
        private void OnDrawGizmos()
        {
            foreach (var gear in gears)
                Gizmos.DrawLine(transform.position, gear.transform.position);
        }
    }
}