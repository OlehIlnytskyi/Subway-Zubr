using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int maxHealth { get; private set; }
    private int health;

    public int maxCoins { get; private set; }
    private int coins;

    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        health = maxHealth = 100;

        maxCoins = 25;
        coins = 0;
        //
        status = ManagerStatus.Started;
    }
    public void AddCoins(int value)
    {
        if (coins >= maxCoins)
        {
            Debug.Log("You already have MaxCoins!!!");
            return;
        }
        coins += value;
        Managers.UIManager.SetCoinsUI(coins);
    }
    public void AddHealth(int value)
    {
        health += value;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            Death();
        }

        Managers.UIManager.SetHealthUI(health);
    }

    private void Death()
    {
        //GetComponent<Renderer>().material.color = Color.red; «робити дл€ ≥грока
    }
}