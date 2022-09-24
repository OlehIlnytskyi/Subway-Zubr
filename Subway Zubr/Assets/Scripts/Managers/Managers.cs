using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PathManager))]
[RequireComponent(typeof(UIManager))]
public class Managers : MonoBehaviour
{
    public static float multiplayer { get; private set; }
    //-------------------------------------------------
    public static PlayerManager PlayerManager { get; private set; }
    public static PathManager PathManager { get; private set; }
    public static UIManager UIManager { get; private set; }
    //public static SoundManager SoundManager { get; private set; }

    //-------------------------------------------------

    private List<IGameManager> _startSequence;

    //-------------------------------------------------
    public void Exit()
    {
        Application.Quit();
    }
    void Awake()
    {
        multiplayer = 1.0f;

        PlayerManager = GetComponent<PlayerManager>();
        PathManager = GetComponent<PathManager>();
        UIManager = GetComponent<UIManager>();
        //SoundManager = GetComponent<SoundManager>();

        _startSequence = new List<IGameManager>();

        _startSequence.Add(PlayerManager);
        _startSequence.Add(PathManager);
        _startSequence.Add(UIManager);
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
    private void Update()
    {
        multiplayer += (Time.deltaTime / 40) / multiplayer;
        //Debug.Log(multiplayer);
    }
}