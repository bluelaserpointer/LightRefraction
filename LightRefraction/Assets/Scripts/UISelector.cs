using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class UISelector : MonoBehaviour
    {
        [SerializeField]
        Selectable selectable;
        [SerializeField]
        bool selectOnAwake, selectOnEnable;

        private void Start()
        {
            if(selectOnAwake)
                DoSelect();
        }
        private void OnEnable()
        {
            if (selectOnEnable)
                DoSelect();
        }
        public void DoSelect()
        {
            selectable?.Select();
        }
    }
}