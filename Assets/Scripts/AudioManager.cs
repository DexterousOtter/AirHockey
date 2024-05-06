using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip puckCollision;
    public AudioClip goal;
    public AudioClip LostGame;
    public AudioClip WonGame;

    private AudioSource audioScource;

    private void Start()
    {
        audioScource = GetComponent<AudioSource>();

    }
    public void PlayPuckCollision()
    {
        audioScource.PlayOneShot(puckCollision);

    }

    public void PlayGoal()
    {
        audioScource.PlayOneShot(goal);
    }
    public void PlayLostGame()
    {
        audioScource.PlayOneShot(LostGame);
    }
    public void PlayWonGame()
    {
        audioScource.PlayOneShot(WonGame);
    }
}