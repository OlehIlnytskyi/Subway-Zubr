using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderCoins;
    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        sliderHealth.value = sliderHealth.maxValue = Managers.PlayerManager.maxHealth;
        sliderHealth.GetComponentInChildren<TextMeshProUGUI>().text = sliderHealth.value + "/" + sliderHealth.maxValue;


        sliderCoins.value = 0;
        sliderCoins.maxValue = Managers.PlayerManager.maxCoins;
        sliderCoins.GetComponentInChildren<TextMeshProUGUI>().text = sliderCoins.value + "/" + sliderCoins.maxValue;
        //
        status = ManagerStatus.Started;
    }
    public void SetCoinsUI(int coins)
    {
        sliderCoins.value = coins;
        sliderCoins.GetComponentInChildren<TextMeshProUGUI>().text = coins.ToString() + "/" + sliderCoins.maxValue;
    }
    public void SetHealthUI(int health)
    {
        sliderHealth.value = health;
        sliderHealth.GetComponentInChildren<TextMeshProUGUI>().text = health.ToString() + "/" + sliderHealth.maxValue;

        Color color = sliderHealth.GetComponentInChildren<Image>().color;
        color.r = (135f + health) / 255;
        sliderHealth.GetComponentInChildren<Image>().color = color;
    }
}