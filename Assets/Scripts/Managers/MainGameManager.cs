using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public GameConfig gameConfig;
    public AudioSource audio_backgroundMusic;

    private void Awake()
    {
        Debug.Log("Awake");
        DontDestroyOnLoad(this);
        Instance = this;
        GameRandom.Core = new DefaultRandom();
    }


    private IEnumerator Start()
    {
        Debug.Log("Start");
        //audio_backgroundMusic.Play();
        yield return null;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Quit");
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
