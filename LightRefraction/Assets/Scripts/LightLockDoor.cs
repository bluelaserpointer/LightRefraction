using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class LightLockDoor : MonoBehaviour
    {
        [SerializeField]
        List<LightBlocker> lightBlockers = new List<LightBlocker>();
        [SerializeField]
        GameObject door;

        public bool IsLocked => door.activeSelf;
        void Update()
        {
            LightBlocker notLightenObj = lightBlockers.Find(blocker => !blocker.IsLighten);
            if (IsLocked)
            {
                if (notLightenObj == null)
                    Lock(false);
            }
            else
            {
                if (notLightenObj != null)
                    Lock(true);
            }
        }
        private void Lock(bool cond)
        {
            door.SetActive(cond);
        }
        private void OnDrawGizmos()
        {
            if (lightBlockers.Count == 0 || door == null)
                return;
            Gizmos.color = Color.magenta;
            foreach(LightBlocker blocker in lightBlockers)
            {
                Gizmos.DrawLine(door.transform.position, blocker.transform.position);
            }
        }
    }

}