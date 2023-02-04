using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _audioSource.Play();
        _spriteRenderer.color = Color.clear;
    }
}