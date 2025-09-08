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
    [SerializeField] private BallAudio ballAudio;
    [SerializeField] private ParticleSystem collisionParticle;
    public float maxCollisionAngle = 45f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
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

        EmitParticle(32);
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
            GameManager.Instance.screenShake.StartShake(0.33f, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Paddle paddle))
        {
            rb2D.linearVelocity *= speedMultiplier;
            ballAudio.PlayPaddleSound();
            EmitParticle(16);
            AdjustAngle(paddle, collision);
            GameManager.Instance.screenShake.StartShake(Mathf.Sqrt(rb2D.linearVelocity.magnitude) * 0.02f, 0.05f);
        }

        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            ballAudio.PlayWallSound();
            EmitParticle(8);
            GameManager.Instance.screenShake.StartShake(0.033f, 0.033f);
        }
    }

    private void AdjustAngle(Paddle paddle, Collision2D collision2D)
    {
        Vector2 median = Vector2.zero;
        foreach (ContactPoint2D contact in collision2D.contacts)
        {
            median += contact.point;
        }

        median /= collision2D.contactCount;

        float absoluteDistanceFromCenter = median.y - paddle.transform.position.y;
        float relativeDistanceFromCenter = absoluteDistanceFromCenter * 2 / paddle.transform.localScale.y;

        int angleSign = paddle.IsLeftPaddle() ? 1 : -1;
        float angle = relativeDistanceFromCenter * maxCollisionAngle * angleSign;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector2 dir = paddle.IsLeftPaddle() ? Vector2.right : Vector2.left;
        Vector2 velocity = rot * dir * rb2D.linearVelocity.magnitude;

        rb2D.linearVelocity = velocity;
    }

    private void EmitParticle(int amount)
    {
        collisionParticle.Emit(amount);
    }
}
