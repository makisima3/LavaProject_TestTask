using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code
{
    public class LevelRestart : MonoBehaviour
    {
        [SerializeField] private Button restartLevel;

        private void Awake()
        {
            restartLevel.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}