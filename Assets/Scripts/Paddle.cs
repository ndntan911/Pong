using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private int id;
    [SerializeField] private float moveSpeed = 2f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movement = ProcessInput();
        Move(movement);
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

        velocity.y = movement * moveSpeed;

        rb2D.linearVelocity = velocity;
    }
}
