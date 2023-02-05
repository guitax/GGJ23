using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public bool IsPlaying { get; private set; }
    public GameScore GameScore { get; set; }

    public GameConfig gameConfig;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);
        Instance = this;

        GameRandom.Core = new DefaultRandom();
    }

    private void Start()
    {
        StopPlay();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Quit();
        }
    }

    public void StartPlay()
    {
        GameScore = new GameScore();
        Time.timeScale = 1f;
        IsPlaying = true;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopPlay()
    {
        Time.timeScale = 0f;
        IsPlaying = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Destroy()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
