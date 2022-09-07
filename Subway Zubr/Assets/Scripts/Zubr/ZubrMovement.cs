using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubrMovement : MonoBehaviour
{
    private float roadSize;
    private float sideSpeed;
    private Vector3 pos;
    private float sliding;
    private Side toSide;

    private void Start()
    {
        roadSize = 2.0f;
        pos = transform.position;
        sideSpeed = 3f;
    }
    void Update()
    {
        if (toSide != Side.Center)
        {
            GoSide();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (pos.x == 0f || pos.x == roadSize)
            {
                sliding = roadSize;
                toSide = Side.Left;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (pos.x == 0f || pos.x == -roadSize)
            {
                sliding = roadSize;
                toSide = Side.Right;
            }
        }
    }
    private void GoSide()
    {
        sliding -= sideSpeed * Time.deltaTime;
        pos.x += sideSpeed * Time.deltaTime * (int)toSide;
        transform.position = pos;

        if (sliding <= 0)
        {
            sliding = 0;
            toSide = Side.Center;
            pos.x = (int)pos.x;
            transform.position = pos;
        }
    }
}