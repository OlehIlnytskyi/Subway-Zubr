using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderCoins;
    [SerializeField] private Slider sliderItem;
    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        sliderHealth.value = sliderHealth.maxValue = Managers.PlayerManager.maxHealth;
        sliderHealth.GetComponentInChildren<TextMeshProUGUI>().text = sliderHealth.value + "/" + sliderHealth.maxValue;


        sliderCoins.value = 0;
        sliderCoins.maxValue = Managers.PlayerManager.maxCoins;
        sliderCoins.GetComponentInChildren<TextMeshProUGUI>().text = sliderCoins.value + "/" + sliderCoins.maxValue;

        sliderItem.value = 0f;
        sliderItem.maxValue = 8.0f;
        sliderItem.gameObject.SetActive(false);
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
    public void SetItemUI(Item item)
    {
        sliderItem.value = sliderItem.maxValue;
        sliderItem.gameObject.SetActive(true);
        StartCoroutine(ActivateItem());
    }
    private IEnumerator ActivateItem()
    {
        yield return new WaitForFixedUpdate();

        if (sliderItem.value > 0)
        {
            sliderItem.value -= Time.deltaTime;
            sliderItem.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, sliderItem.value / sliderItem.maxValue);
            yield return StartCoroutine(ActivateItem());
        }
        else
        {
            sliderItem.value = 0;
            sliderItem.gameObject.SetActive(false);
        }
    }
}