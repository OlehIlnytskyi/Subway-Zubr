using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public float multiplayer { get; private set; }

    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        multiplayer = 1.0f;
        //
        status = ManagerStatus.Started;
    }

    private void Update()
    {
        multiplayer += (Time.deltaTime / 50f) / multiplayer;
    }
}
