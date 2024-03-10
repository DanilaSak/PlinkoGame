using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Game2
{
    public class Game2WinView : MonoBehaviour
    {
        public static Game2WinView i;

        [SerializeField] private TextMeshProUGUI winTriggerText;
        [SerializeField] private Image winTriggerimage;
        private void Awake()
        {
            i = this;
        }

        public void SetWinTarget(BallTrigger trigger)
        {
            winTriggerimage.color = trigger._color;
            winTriggerText.text = trigger.text.text;
        }
    }
}