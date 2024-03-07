using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class WalletDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private BallWallet _wallet;



        private void OnChange()
        {
            if (!text || !_wallet) return;
            text.text = $"{_wallet.Count}";
        }

        public void SetWallet(BallWallet gameWallet)
        {
            _wallet = gameWallet;
            _wallet.Change += OnChange;

            OnChange();
        }
    }
}