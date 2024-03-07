using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class GameSpawner : MonoBehaviour
    {
        [SerializeField] private Game gamePrefab;
        [SerializeField] private Game game;
        [SerializeField] private GameUI gameUI;
        public void Spawn()
        {
          game =  Instantiate(gamePrefab);
          gameUI.SetGame(game);
        }

        public void Despawn()
        {
            if (!game)return;
            Destroy(game.gameObject);
        }
    }
}