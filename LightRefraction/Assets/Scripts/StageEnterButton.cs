using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class StageEnterButton : MonoBehaviour
    {
        [SerializeField]
        Stage stage;

        public Button Button { get; private set; }
        private void Awake()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(() =>
            {
                GameManager.selectedStage = stage;
                SceneManager.LoadScene("Game");
            });
        }

        private void OnValidate()
        {
            if(stage != null)
                gameObject.name = stage.name;
        }
    }
}