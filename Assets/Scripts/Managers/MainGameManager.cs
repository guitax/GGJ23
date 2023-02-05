using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public bool IsPlaying { get; private set; }
    public GameScore GameScore { get; set; }

    public GameConfig gameConfig;

    public AudioSource audio_backgroundMusic;

    private void Awake()
    {
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

        if (!audio_backgroundMusic.isPlaying)
        {
            audio_backgroundMusic.Play();
        }
    }

    public void StopPlay()
    {
        Time.timeScale = 0;
        IsPlaying = false;
    }

    private static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
