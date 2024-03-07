using System;
using Doozy.Engine.Soundy;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelCounter : MonoBehaviour
    {
        private int level;
        public string key;

        [SerializeField] private TextMeshProUGUI text;

        public int Level
        {
            get => PlayerPrefs.GetInt("Level" + key, 1);
            set => PlayerPrefs.SetInt("Level" + key, value);
        }

        public static void AddLevel(string key)
        {
            var Level= PlayerPrefs.GetInt("Level" + key, 1);
            Level++;
            Debug.Log($"{Level} / {key}");
           
            PlayerPrefs.SetInt("Level" + key, Level);
        }

        private void OnEnable()
        {
            if (text)
            {
                text.text = $"Level: {Level}";
            }
        }
    }
}