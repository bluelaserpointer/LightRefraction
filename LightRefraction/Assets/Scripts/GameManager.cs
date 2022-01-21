using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        //inspector
        [Header("Debug")]
        [SerializeField]
        Stage debugStage;

        [Header("Scene Reference")]
        [SerializeField]
        Transform worldTransform;
        [SerializeField]
        Transform lightLinesGroupTransform;
        [SerializeField]
        Player player;

        [Header("Assets Reference")]
        [SerializeField]
        LightLine lightLinePrefab;

        //data
        public static GameManager Instance { get; private set; }
        public Stage GeneratedStage { get; private set; }
        private void Awake()
        {
            Instance = this;
            //debug
            LoadStage(debugStage);
        }
        public void LoadStage(Stage stage)
        {
            if(GeneratedStage != null)
                Destroy(GeneratedStage);
            GeneratedStage = Instantiate(stage, worldTransform);
            player.transform.position = stage.startPosition.position;
        }
        public void EmitLightLine(LightLine lightLine, Vector2 origin, Vector2 direction)
        {
        }
    }
}