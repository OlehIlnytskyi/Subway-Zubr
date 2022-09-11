using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private GameObject[] roads;

    private List<GameObject> currentPath;
    private int pathLength;
    private Vector3 pos;

    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        pathLength = 3;
        pos = new Vector3(0, 0, 45);
        currentPath = new List<GameObject>();

        for (int i = 0; i < pathLength; i++)
        {
            AddRoad();
        }

        //
        status = ManagerStatus.Started;
    }
    public void AddRoad()
    {
        if (currentPath.Count < 3)
        {
            GameObject road = Instantiate(GetRoad());
            road.transform.position = pos;
            pos.z += 90;
            currentPath.Add(road);
        }
        else
        {
            StartCoroutine(NewRoad());
        }
    }
    private IEnumerator NewRoad()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject road = currentPath[0];
        currentPath.RemoveAt(0);
        Destroy(road);
        AddRoad();
    }
    private GameObject GetRoad()
    {
        GameObject road = roads[Random.Range(0, roads.Length)];

        foreach (GameObject value in currentPath)
        {
            if (value.tag == road.tag)
            {
                return GetRoad();
            }
        }

        return road;
    }
}