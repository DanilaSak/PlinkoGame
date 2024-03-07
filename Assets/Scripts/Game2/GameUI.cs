using UnityEngine;

namespace DefaultNamespace.Game2
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private WalletDisplay walletDisplay;
        [SerializeField] private GameWin gameWin;

        public void SetGame(Game game)
        {
            walletDisplay.SetWallet(game.wallet);
            gameWin.SetWallet(game.wallet);
            
        }
    }
}