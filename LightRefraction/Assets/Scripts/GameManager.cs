using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        [SerializeField]
        SBA_FadeIO stageChangingFader;

        [Header("Assets Reference")]
        [SerializeField]
        LightLine lightLinePrefab;

        //data
        public static GameManager Instance { get; private set; }
        private static List<Stage> _stages;
        public static List<Stage> Stages => _stages ?? (_stages = new List<Stage>(Resources.LoadAll<Stage>("Stages")));
        public static Stage selectedStage;
        public Stage GeneratedStage { get; private set; }
        public static Player Player => Instance.player;
        private void Awake()
        {
            Instance = this;
            //debug
            LoadStage(selectedStage ?? (selectedStage = debugStage));
        }
        private void Start()
        {
            stageChangingFader.CanvasGroup.alpha = 1;
            stageChangingFader.FadeOut();
        }
        public void LoadStage(Stage stage)
        {
            if(GeneratedStage != null)
                Destroy(GeneratedStage);
            GeneratedStage = Instantiate(stage, worldTransform);
            player.transform.position = stage.startPosition.position;
        }
        public void NextStage()
        {
            int currentID = Stages.IndexOf(selectedStage);
            if(currentID == -1)
            {
                Debug.LogWarning("NextStage: Invalid seleted stage!");
                return;
            }
            if(currentID == Stages.Count - 1)
            {
                //TODO: game clear
                Debug.LogWarning("NextStage: All stage cleared!");
                return;
            }
            selectedStage = Stages[currentID + 1];
            stageChangingFader.FadeIn(() =>
            {
                SceneManager.LoadScene("Game");
            });
        }
        public void EmitLightLine(Vector2 origin, Vector2 direction, LightBlocker ignoreBlocker = null, float distance = 100)
        {
            if (distance <= 0)
                return;
            Vector2 end = Vector2.zero;
            bool foundEnd = false;
            foreach(RaycastHit2D hitInfo in Physics2D.RaycastAll(origin, direction, distance))
            {
                LightBlocker lightBlocker = hitInfo.collider.gameObject.GetComponent<LightBlocker>();
                if (lightBlocker != null && !lightBlocker.Equals(ignoreBlocker))
                {
                    if (lightBlocker.doBlock)
                    {
                        end = hitInfo.point;
                        float remainDistance = distance - hitInfo.distance;
                        if (lightBlocker.doReflection)
                        {
                            EmitLightLine(end, Vector2.Reflect(direction, hitInfo.normal), lightBlocker, remainDistance);
                        }
                        foundEnd = true;
                        lightBlocker.Lighten(end, direction, remainDistance);
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