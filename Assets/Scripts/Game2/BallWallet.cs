using System;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class BallWallet : MonoBehaviour
    {
        [SerializeField] private  int count;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                Change?.Invoke();
            }
        }

        public event Action Change;
    }
}