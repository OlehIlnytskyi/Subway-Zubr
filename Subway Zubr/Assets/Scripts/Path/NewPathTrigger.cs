using UnityEngine;
public class NewPathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.main.PathManager.AddRoad();
    }
}