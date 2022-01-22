using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LightBlocker), typeof(Interactable))]
    public class GemSlot : MonoBehaviour
    {
        public Gem HoldingGem { get; private set; }

        public LightBlocker LightBlocker { get; private set; }
        private void Awake()
        {
            LightBlocker = GetComponent<LightBlocker>();
            LightBlocker.OnLighten.AddListener(OnLighten);
            GetComponent<Interactable>().OnInteract.AddListener(OnInteract);
            HoldingGem = GetComponentInChildren<Gem>();
            OnGemSet();
        }
        public void OnInteract()
        {
            Gem tmpGem = GameManager.Player.HoldingGem;
            GameManager.Player.HoldingGem = HoldingGem;
            if((HoldingGem = tmpGem) != null)
            {
                HoldingGem.transform.SetParent(transform);
                HoldingGem.transform.localPosition = Vector3.zero;
            }
            OnGemSet();
        }
        private void OnGemSet()
        {
            LightBlocker.doBlock = HoldingGem != null;
        }
        private void OnLighten(Vector2 point, Vector2 direction, float remainDistance)
        {
            if (HoldingGem == null)
                return;
            foreach(float fractionAngle in HoldingGem.DegreeAngles)
            {
                float fractionRadian = fractionAngle * Mathf.Deg2Rad;
                float cos = Mathf.Cos(fractionRadian), sin = Mathf.Sin(fractionRadian);
                Vector2 fractionDirection = new Vector2(direction.x * cos + direction.y * sin, -direction.x * sin + direction.y * cos);
                GameManager.Instance.EmitLightLine(point, fractionDirection, LightBlocker, remainDistance);
            }
        }
    }
}