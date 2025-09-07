using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float maxInitialAngle = 0.67f;
    [SerializeField] private float moveSpeed = 1f;
    private float startX = 0f;
    private float maxStartY = 4f;
    [SerializeField] private float speedMultiplier = 1.1f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InitialPush();
        GameManager.Instance.OnReset += GameManager_OnReset;
    }

    private void GameManager_OnReset()
    {
        ResetBallPosition();
        InitialPush();
    }

    private void InitialPush()
    {
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;

        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2D.linearVelocity = dir * moveSpeed;
    }

    private void ResetBallPosition()
    {
        float positionY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, positionY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ScoreZone scoreZone))
        {
            GameManager.Instance.OnScoreZoneReached(scoreZone.id);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Paddle paddle))
        {
            rb2D.linearVelocity *= speedMultiplier;
        }
    }
}
