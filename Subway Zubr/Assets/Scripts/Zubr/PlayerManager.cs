using UnityEngine;
public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private int coins;
    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        coins = 0;
        //
        status = ManagerStatus.Started;
    }
    public void AddCoins(int value)
    {
        coins += value;
        Managers.UIManager.AddCoinsUI(coins);
    }
}