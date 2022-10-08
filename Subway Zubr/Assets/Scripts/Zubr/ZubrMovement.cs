using UnityEngine;

public class ZubrMovement : MonoBehaviour
{
    // ���
    private Vector3     movement;
    private float       moveSpeed;

    // ���������� � �������
    private Side        toSide;
    private float       roadOffsetX;    // ������ ���� ������
    private float       sliding;        // �� ��� ������� ������� ����������

    // �������
    private bool        jumping;        // �������� �� �������� � �����
    private bool        grounded;       // �� �������� �����������
    private float       gravity;        // ���� ������
    private float       jumpHeight;

    private void Start()
    {
        // ���������� � �������
        toSide = Side.None;
        roadOffsetX = 2.0f;
        sliding = 0f;

        // �������
        gravity = 6.0f;
        jumpHeight = 3.0f;
    }
    void Update()
    {
        movement = Vector3.zero;

        MoveForward();
        MoveSide();
        MoveUp();
        MoveDown();

        transform.Translate(movement * Time.deltaTime, Space.World);
    }

    public void Jump()
    {
        if (!jumping && grounded)
        {
            GetComponent<ParticleSystem>().Play();
            gravity = 6.0f;
            jumping = true;
            grounded = false;
        }
    }

    public void GoLeft()
    {
        if (transform.position.x == 0f || transform.position.x == roadOffsetX)
        {
            toSide = Side.Left;
        }
    }

    public void GoRight()
    {
        if (transform.position.x == 0f || transform.position.x == -roadOffsetX)
        {
            toSide = Side.Right;
        }
    }

    private void MoveForward()
    {
        moveSpeed = Managers.main.PlayerManager.speed * Managers.main.GameManager.multiplayer;
        movement.z = moveSpeed;
    }

    private void MoveUp()
    {
        if (jumping == false)
        {
            return;
        }

        if (transform.position.y >= jumpHeight)
        {
            jumping = false;
        }

        gravity -= (3 - Managers.main.GameManager.multiplayer / jumpHeight) * Time.deltaTime;
        movement.y += gravity;
    }

    private void MoveDown()
    {
        if (grounded == false && jumping == false)
        {
            if (gravity < 6.0f)
            {
                gravity += Time.deltaTime * 4;
            }

            movement.y -= gravity;
        }
    }

    private void MoveSide()
    {
        if (toSide == Side.None)
        {
            return;
        }

        if (sliding <= -roadOffsetX || sliding >= roadOffsetX)
        {
            sliding = 0;
            toSide = Side.None;

            Vector3 pos = transform.position;
            pos.x = Mathf.Round(pos.x);
            transform.position = pos;
            return;
        }

        movement.x += moveSpeed / 2 * ((int)toSide);
        sliding    += movement.x * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}