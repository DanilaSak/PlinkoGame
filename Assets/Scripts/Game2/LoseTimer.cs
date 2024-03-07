using System;
using System.Collections;
using Doozy.Engine;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class LoseTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float StartTime = 10;
        [SerializeField] private float time;

        private void OnEnable()
        {
            time = StartTime;
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (time > 0)
            {
                yield return null;
                time -= Time.deltaTime;
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                string mm = timeSpan.Minutes.ToString("D2");
                string ss = timeSpan.Seconds.ToString("D2");
                text.text = $"TIME: {mm}:{ss}";
            }

            GameEventMessage.SendEvent("Lose");
        }
    }
}