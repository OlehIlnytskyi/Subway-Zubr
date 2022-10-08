using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers main          { get; private set; }

    public GameManager   GameManager     { get; private set; }
    public PlayerManager PlayerManager   { get; private set; }
    public UIManager     UIManager       { get; private set; }
    public PathManager   PathManager     { get; private set; }
    public ItemsManager  ItemsManager    { get; private set; }
    //public static SoundManager SoundManager { get; private set; }

    private List<IGameManager> _startSequence;

    public void Exit()
    {
        Application.Quit();
    }

    void Awake()
    {
        if (main != null && main != this)
        {
            Debug.LogWarning("Deleting main");
            Destroy(this);
            return;
        }
        else
        {
            main = this;
        }

        PlayerManager = GetComponentInChildren<PlayerManager>();
        PathManager = GetComponentInChildren<PathManager>();
        UIManager = GetComponentInChildren<UIManager>();
        ItemsManager = GetComponentInChildren<ItemsManager>();
        GameManager = GetComponentInChildren<GameManager>();
        //SoundManager = GetComponent<SoundManager>();

        _startSequence = new List<IGameManager>();

        _startSequence.Add(PlayerManager);
        _startSequence.Add(PathManager);
        _startSequence.Add(UIManager);
        _startSequence.Add(ItemsManager);
        _startSequence.Add(GameManager);
        //_startSequence.Add(SoundManager);

        StartCoroutine(StartupManagers());
    }
    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Initialize();
        }
        yield return null;
        int numModules = _startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null;
        }
        Debug.Log("All managers started up");
    }
}