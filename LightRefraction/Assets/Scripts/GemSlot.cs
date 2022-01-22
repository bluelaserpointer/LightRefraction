using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LightBlocker), typeof(Interactable))]
    public class GemSlot : MonoBehaviour
    {
        public Gem holdingGem;

        public LightBlocker LightBlocker { get; private set; }
        private void Awake()
        {
            LightBlocker = GetComponent<LightBlocker>();
            GetComponent<Interactable>().OnInteract.AddListener(OnInteract);
        }
        public void OnInteract()
        {
            Gem tmpGem = GameManager.Player.HoldingGem;
            GameManager.Player.HoldingGem = holdingGem;
            holdingGem = tmpGem;
        }
    }
}