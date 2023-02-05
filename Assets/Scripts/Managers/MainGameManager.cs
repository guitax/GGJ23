using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }
    public bool IsPlaying { get; private set; }

    public GameConfig gameConfig;
   
    public AudioSource audio_backgroundMusic;
    
    [SerializeField]
    private PlayerManager playerManager;
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
        Time.timeScale = 0;
        PlayerManager.SurfaceDeath += OnDeath;

        yield return null;
    }

    private void OnDeath()
    {
        if (!godMode)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("DeathScene");
        }
    }

    private void Update()
    {
        if (!IsPlaying)
        {
            float moveDirection = playerManager.playerInputActions.Player.Move.ReadValue<float>();
            if (moveDirection != 0f)
            {
                IsPlaying = true;
                Time.timeScale = 1;

                if (!audio_backgroundMusic.isPlaying)
                {
                    audio_backgroundMusic.Play();    
                }
            }
        }
        
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
