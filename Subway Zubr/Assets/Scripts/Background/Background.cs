using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector3 pos;
    private float speed = 6.0f;
    private bool toleft;
    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        pos.z += speed * Time.deltaTime;

        if (toleft)
        {
            pos.x -= Time.deltaTime;
            if (pos.x <= -1)
            {
                toleft = !toleft;
            }
        }
        else
        {
            pos.x += Time.deltaTime;
            if (pos.x >= 1)
            {
                toleft = !toleft;
            }
        }

        transform.position = pos;
    }
}
