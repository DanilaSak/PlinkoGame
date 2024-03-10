using System;
using Doozy.Engine;
using Doozy.Engine.Soundy;
using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class HitEffect : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            SoundyManager.Play("General", "Hit");
            GameEventMessage.SendEvent("Lose");
        }
    }
}