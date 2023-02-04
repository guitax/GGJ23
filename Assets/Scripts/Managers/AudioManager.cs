using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        PlayerManager.AudioPowerDown += PowerDownEvent;
        PlayerManager.AudioPowerUp += PowerUpEvent;
    }

    private void PowerUpEvent()
    {
        AudioSource powerUpAudio = GetComponent<AudioSource>();
        powerUpAudio.clip = Resources.Load<AudioClip>("Audio/Water.mp3");
        powerUpAudio.Play();
    }

    private void PowerDownEvent()
    {
        AudioSource powerDown = GetComponent<AudioSource>();
        powerDown.clip = Resources.Load<AudioClip>("Audio/Rock.mp3");
        powerDown.Play();
    }
    
}