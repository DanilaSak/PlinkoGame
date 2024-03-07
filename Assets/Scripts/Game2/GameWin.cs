using System;
using Doozy.Engine;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class GameWin : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private BallWallet _wallet;
        [SerializeField] private float scale;
        [SerializeField] private int  need;
        [SerializeField] private LevelCounter _levelCounter;
        
        public void SetWallet(BallWallet gameWallet)
        {
            _wallet = gameWallet;
            _wallet.Change += OnChange;
        }

        private bool isWin=false;
        private void OnChange()
        {
            if (isWin)return;
            if (_wallet.Count < need)return;
            isWin = true;
            LevelCounter.AddLevel(_levelCounter.key);
            GameEventMessage.SendEvent("Win");
        }

        private void OnEnable()
        {
            need = Mathf.CeilToInt(_levelCounter.Level * scale);
            text.text = $"{need}";
            isWin = false;
        }
    }
}