using UnityEngine;

public class ItemsManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private float itemDuration;

    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        itemDuration = 5.0f;
        //
        status = ManagerStatus.Started;
    }
    public void ActiveItem(Item item)
    {
        switch (item)
        {
            case Item.Coin:
                Managers.PlayerManager.AddCoins(1);
                break;
            case Item.ToiletPaper:
                Managers.UIManager.SetItemUI(item);

                break;
            case Item.PinkWard:
                Managers.UIManager.SetItemUI(item);

                break;
        }
    }
}