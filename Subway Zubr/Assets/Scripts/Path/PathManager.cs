using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour, IGameManager
{
    //-------------------------------------------------
    public ManagerStatus status { get; private set; }
    [SerializeField] private GameObject[] roads;

    //-------------------------------------------------

    private List<GameObject> currentPath;
    private int pathLength;
    private Vector3 pos;

    //-------------------------------------------------

    [SerializeField] private GameObject[] activeItems;
    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        //
        pathLength = 4;
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
        if (currentPath.Count < pathLength)
        {
            GameObject road = Instantiate(GetRoad());
            road.transform.position = pos;
            pos.z += 90;

            if (Random.Range(0, 100) < 20)
            {
                GameObject activeItem = Instantiate(activeItems[Random.Range(0, activeItems.Length)]);
                activeItem.transform.parent = road.transform;
                activeItem.transform.position = GetActiveItemTransform(road).position;
            }

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
    private Transform GetActiveItemTransform(GameObject road)
    {
        foreach (Transform tr in road.transform)
        {
            if (tr.tag == "ActiveItemPlaceholder")
            {
                Debug.Log("Found!");
                return tr;
            }
        }
        return null;
    }
}