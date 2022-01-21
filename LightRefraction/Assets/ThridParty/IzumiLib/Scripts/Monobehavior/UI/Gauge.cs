using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IzumiLib
{
    [DisallowMultipleComponent]
    public class Gauge : MonoBehaviour
    {
        //inspector
        [SerializeField]
        Image _fillImage;

        //data
        public Image FillImage => _fillImage;
        public float Ratio
        {
            get => _fillImage.fillAmount;
            set
            {
                _fillImage.fillAmount = value;
            }
        }
    }
}