using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PathManager))]
[RequireComponent(typeof(UIManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager PlayerManager { get; private set; }
    public static PathManager PathManager { get; private set; }
    //public static SoundManager SoundManager { get; private set; }
    public static UIManager UIManager { get; private set; }

    private List<IGameManager> _startSequence;
    public void Exit()
    {
        Application.Quit();
    }
    void Awake()
    {
        PlayerManager = GetComponent<PlayerManager>();
        PathManager = GetComponent<PathManager>();
        //SoundManager = GetComponent<SoundManager>();
        UIManager = GetComponent<UIManager>();

        _startSequence = new List<IGameManager>();

        _startSequence.Add(PlayerManager);
        _startSequence.Add(PathManager);
        //_startSequence.Add(SoundManager);
        _startSequence.Add(UIManager);

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