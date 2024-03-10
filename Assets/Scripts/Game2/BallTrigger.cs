using System;
using Doozy.Engine;
using Doozy.Engine.Soundy;
using Doozy.Engine.UI.Animation;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class BallTrigger : MonoBehaviour
    {
        [SerializeField] private BallWallet _wallet;
        [SerializeField] public TextMeshPro text;
        [SerializeField] private SpriteRenderer sp;
        [SerializeField] public Color _color;


        [SerializeField] private float scale;

        private static bool isWin;

        private void Start()
        {
            isWin = false;
            text.text = $"X {scale}";
            sp.color = _color;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isWin) return;
            Destroy(other.gameObject);
            SoundyManager.Play("General", "Trigger");
            isWin = true;
            Game2WinView.i.SetWinTarget(this);
            LevelCounter.AddLevel("Game2");
            GameEventMessage.SendEvent("Win");
        }
    }
}