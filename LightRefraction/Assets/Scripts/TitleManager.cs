using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class TitleManager : MonoBehaviour
    {
        [SerializeField]
        Transform stageButtonGroup;
        [SerializeField]
        StageEnterButton stageButtonPrefab;

        void Start()
        {
            foreach(var stage in GameManager.Stages)
            {
                Instantiate(stageButtonPrefab, stageButtonGroup).stage = stage;
            }
        }
    }

}