using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private int id;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 startPosition;
    public float AIDeadzone = 1f;
    int direction = 0;
    private float moveSpeedMultiplier = 1f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPosition = transform.position;
        GameManager.Instance.OnReset += GameManager_OnReset;
    }

    private void GameManager_OnReset()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        if (IsAI())
        {
            MoveAI();
        }
        else
        {

            float movement = ProcessInput();
            Move(movement);
        }
    }

    private bool IsAI()
    {
        bool IsPlayer1AI = id == 1 && GameManager.Instance.IsPlayer1AI();
        bool IsPlayer2AI = id == 2 && GameManager.Instance.IsPlayer2AI();
        return IsPlayer1AI || IsPlayer2AI;
    }

    private void MoveAI()
    {
        Vector2 ballPos = GameManager.Instance.ball.transform.position;

        if (Mathf.Abs(ballPos.y - transform.position.y) > AIDeadzone)
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }

        Move(direction);
    }

    private float ProcessInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velocity = rb2D.linearVelocity;

        velocity.y = movement * moveSpeed * moveSpeedMultiplier;

        rb2D.linearVelocity = velocity;
    }


    public bool IsLeftPaddle()
    {
        return id == 1;
    }
}
