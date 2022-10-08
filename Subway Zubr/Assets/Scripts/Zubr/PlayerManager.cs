using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private GameObject player;
    [SerializeField] public float speed;

    public int maxHealth        { get; private set; }
    public int maxCoins         { get; private set; }
    private int health;
    private int coins;



    //
    private Colors toColor;
    private float scale;

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
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.GetComponent<ZubrMovement>().Jump();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                player.GetComponent<ZubrMovement>().GoLeft();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player.GetComponent<ZubrMovement>().GoRight();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //player.GetComponent<ZubrMovement>().Slide();
            }
        }




        if (toColor != Colors.None)
        {
            Color green = new Color(0, 0.745f, 0, 1);
            scale += Time.deltaTime * 3;

            switch (toColor)
            {
                case Colors.Red:
                    player.GetComponent<Renderer>().material.color = Color.Lerp(green, Color.red, scale);
                    if (scale >= 1) { toColor = Colors.Green; scale = 0; }
                    break;
                case Colors.Green:
                    player.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, green, scale);
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
        Managers.main.UIManager.SetCoinsUI(coins);
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

        Managers.main.UIManager.SetHealthUI(health);
    }
    private void Death()
    {
        player.GetComponent<Renderer>().material.color = Color.red;
    }
}
enum Colors
{
    None,
    Red,
    Green
}