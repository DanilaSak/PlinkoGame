using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class WalletDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public static WalletDisplay i;
        private void Awake()
        {
            i = this;
        }

        public void Change(int count)
        {
            if (!text ) return;
            text.text = $"{count}";
        }

    }
}