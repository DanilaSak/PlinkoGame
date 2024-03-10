using System;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int ballCount;

        public void Start()
        {
            WalletDisplay.i.Change(ballCount);
        }
    }
}