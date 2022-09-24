using UnityEngine;

public class Box : MonoBehaviour
{
    private void Start()
    {
        Vector3 rotation = Vector3.zero;
        rotation.x = Random.Range(0, 5) * 90f + 2f * Managers.multiplayer;
        rotation.z = Random.Range(0, 5) * 90f + 2f * Managers.multiplayer;
        rotation.y = Random.Range(-30f, 31f);
        gameObject.transform.eulerAngles = rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Managers.PlayerManager.AddHealth(-25);
            Destroy(gameObject);
        }
    }
}
