using System;
using Doozy.Engine.Soundy;
using Doozy.Engine.UI.Animation;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class BallTrigger : MonoBehaviour
    {
        [SerializeField] private BallWallet _wallet;
        [SerializeField] private TextMeshPro text;

        [SerializeField] private int scale;

        private void Start()
        {
            text.text = $"X {scale}";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
            SoundyManager.Play("General", "Trigger");
            _wallet.Count += scale;
        }
    }
}