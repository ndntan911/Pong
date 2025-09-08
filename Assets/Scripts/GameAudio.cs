using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource asSounds;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip winSound;

    public void PlayScoreSound() => asSounds.PlayOneShot(scoreSound);
    public void PlayWinSound() => asSounds.PlayOneShot(winSound);
}

