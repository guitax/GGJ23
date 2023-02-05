using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public GameConfig gameConfig;
    public AudioSource audio_backgroundMusic;
    [SerializeField]
    private bool godMode;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        GameRandom.Core = new DefaultRandom();
    }


    private IEnumerator Start()
    {
        audio_backgroundMusic.Play();
        PlayerManager.SurfaceDeath += OnDeath;

        yield return null;
    }

    private void OnDeath()
    {
        if (!godMode)
        {
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Quit();
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
}
