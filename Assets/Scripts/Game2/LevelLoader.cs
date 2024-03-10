using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> levels = new List<GameObject>();

        [SerializeField] private LevelCounter levelCounter;
        private void Awake()
        {
            var index = levelCounter.Level % levels.Count;

            Instantiate(levels[index], transform);
        }
    }
}