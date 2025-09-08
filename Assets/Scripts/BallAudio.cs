using UnityEngine;

public class BallAudio : MonoBehaviour
{
    [SerializeField] private AudioSource asSounds;
    [SerializeField] private AudioClip wallSound;
    [SerializeField] private AudioClip paddleSound;

    public void PlayWallSound() => asSounds.PlayOneShot(wallSound);
    public void PlayPaddleSound() => asSounds.PlayOneShot(paddleSound);
}
