using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private Colors toColor;
    private float scale;
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
    private void Update()
    {
        if (toColor != Colors.None)
        {
            Color green = new Color(0, 0.745f, 0, 1);
            scale += Time.deltaTime * 3;

            switch (toColor)
            {
                case Colors.Red:
                    Managers.player.GetComponent<Renderer>().material.color = Color.Lerp(green, Color.red, scale);
                    if (scale >= 1) { toColor = Colors.Green; scale = 0; }
                    break;
                case Colors.Green:
                    Managers.player.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, green, scale);
                    if (scale >= 1) { toColor = Colors.None; scale = 0; }
                    break;
            }
        }
    }
    public void AddCoins(int value)
    {
        if (coins >= maxCoins)
        {
            return;
        }
        coins += value;
        Managers.UIManager.SetCoinsUI(coins);
    }
    public void AddHealth(int value)
    {
        health += value;

        if (value < 0)
        {
            toColor = Colors.Red;
        }

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
        Managers.player.GetComponent<Renderer>().material.color = Color.red;
    }
}
enum Colors
{
    None,
    Red,
    Green
}