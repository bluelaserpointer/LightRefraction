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
        public static Stage selectedStage;
        public Stage GeneratedStage { get; private set; }
        public static Player Player => Instance.player;
        private void Awake()
        {
            Instance = this;
            //debug
            LoadStage(selectedStage ?? debugStage);
        }
        public void LoadStage(Stage stage)
        {
            if(GeneratedStage != null)
                Destroy(GeneratedStage);
            GeneratedStage = Instantiate(stage, worldTransform);
            player.transform.position = stage.startPosition.position;
        }
        public void EmitLightLine(Vector2 origin, Vector2 direction, LightBlocker ignoreBlocker = null, float distance = 100)
        {
            if (distance <= 0)
                return;
            Vector2 end = Vector2.zero;
            bool foundEnd = false;
            foreach (RaycastHit2D hitInfo in Physics2D.RaycastAll(origin, direction, distance))
            {
                LightBlocker lightBlocker = hitInfo.collider.gameObject.GetComponent<LightBlocker>();
                if (lightBlocker != null && !lightBlocker.Equals(ignoreBlocker))
                {
                    if (lightBlocker.doBlock)
                    {
                        end = hitInfo.point;
                        if (lightBlocker.doReflection)
                        {
                            EmitLightLine(end, Vector2.Reflect(direction, hitInfo.normal), lightBlocker, distance - hitInfo.distance);
                        }
                        foundEnd = true;
                        lightBlocker.Lighten(end, direction);
                        break;
                    }
                }
            }
            if (!foundEnd)
                end = origin + direction * distance;
            LightLine lightLine = Instantiate(lightLinePrefab, lightLinesGroupTransform);
            lightLine.Init(origin, end);
        }
        public void UpdateGemUI()
        {
            //TODO:
        }
    }
}