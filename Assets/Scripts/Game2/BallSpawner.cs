using Doozy.Engine.Soundy;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.Game2
{
    public class BallSpawner : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private BallWallet _wallet;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_wallet.Count < 1) return;
            SpawnBall();
            _wallet.Count--;
        }

        private void SpawnBall()
        {
            var newBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity, transform);
            var x = Random.value * 2 - 1;
            newBall.GetComponent<Rigidbody2D>()?.AddForce(Vector2.right * x *5);
            SoundyManager.Play("General", "Spawn");

        }
    }
}