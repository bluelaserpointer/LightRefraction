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
        public Stage stage;

        public Button Button { get; private set; }
        private void Awake()
        {
            Button = GetComponent<Button>();
        }
        private void Start()
        {
            Button.onClick.AddListener(() =>
            {
                GameManager.selectedStage = stage;
                SceneManager.LoadScene("Game");
            });
            GetComponentInChildren<Text>().text = stage.name;
        }
    }
}