using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private TextMeshProUGUI coinsUI;
    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        coinsUI.text = "- 0";
        //
        status = ManagerStatus.Started;
    }
    public void AddCoinsUI(int value)
    {
        coinsUI.text = "- " + value;
    }
}