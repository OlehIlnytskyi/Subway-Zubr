using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector3 pos;
    private bool toleft;
    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        pos.z += Managers.main.PlayerManager.speed * Managers.main.GameManager.multiplayer * Time.deltaTime;

        if (toleft)
        {
            pos.x -= Time.deltaTime;
            if (pos.x <= -2)
            {
                toleft = !toleft;
            }
        }
        else
        {
            pos.x += Time.deltaTime;
            if (pos.x >= 2)
            {
                toleft = !toleft;
            }
        }

        transform.position = pos;
    }
}
