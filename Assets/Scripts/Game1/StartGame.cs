using System;
using UnityEngine;

namespace DefaultNamespace.Game1
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private LevelManager LevelManager;


        private void Start()
        {
            LevelManager.currentLevel = 1;
            LevelManager.LoadLevel();
            
            PlayerPrefs.SetInt("OpenLevelTest", 1);
            PlayerPrefs.SetInt("OpenLevel", 1);
            PlayerPrefs.Save();
          

            LevelManager.LoadLevel();
        }
    }
}