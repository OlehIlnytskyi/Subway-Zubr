using UnityEngine;

public class ZubrMovement : MonoBehaviour
{
    // Рух
    public static float movementSpeed { get; private set; }
    private float startSpeed;   // Базова швидкість
    private Vector3 movement;

    // Переміщення в сторони
    private Side toSide;
    private float roadOffsetX;  // Ширина однієї доріжки
    private float sideSpeed;    // Швидкість переміщення між доріжками
    private float sliding;      // На яку відстань потрібно посунутись
    private bool toRound;       // Заокруглення переміщення в сторони

    // Стрибок
    private bool jumping;   // Перевіряє чи персонаж у повітрі
    private bool grounded;  // Чи персонаж приземлений
    private float gravity;  // Сила тяжіння

    private void Start()
    {
        // Переміщення в сторони
        toSide = Side.Center;
        roadOffsetX = 2.0f;
        sideSpeed = 3f;
        startSpeed = 25.0f;

        // Стрибок
        gravity = 6.0f;
    }
    void Update()
    {
        movement = Vector3.zero;
        // Рух
        movementSpeed = startSpeed * Managers.multiplayer;
        movement.z += movementSpeed;

        // Пемеріщення в сторони
        if (toSide != Side.Center)
        {
            GoSide();
        }

        // Стрибок
        if (jumping)
        {
            gravity -= Time.deltaTime * 3;
            movement.y += gravity;

            if (transform.position.y >= 3f)
            {
                jumping = false;
            }
        }
        else if (!jumping && !grounded)
        {
            if (gravity < 6.0f)
            {
                gravity += Time.deltaTime * 4;
            }
                movement.y -= gravity;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x == 0f || transform.position.x == roadOffsetX)
            {
                sliding = roadOffsetX;
                toSide = Side.Left;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x == 0f || transform.position.x == -roadOffsetX)
            {
                sliding = roadOffsetX;
                toSide = Side.Right;
            }
        }

        // Стрибок
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!jumping && grounded)
            {
                GetComponent<ParticleSystem>().Play();
                gravity = 6.0f;
                jumping = true;
                grounded = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (grounded)
            {
            }
        }

        transform.Translate(movement * Time.deltaTime, Space.World);

        if (toRound)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Round(pos.x);
            transform.position = pos;
            toRound = false;
        }
    }
    private void GoSide()
    {
        sliding -= sideSpeed * Time.deltaTime * Managers.multiplayer;
        movement.x += sideSpeed * (int)toSide * Managers.multiplayer;

        if (sliding <= 0)
        {
            sliding = 0;
            toSide = Side.Center;
            toRound = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}