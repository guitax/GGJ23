using System.Collections;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public GameConfig gameConfig;
    public AudioSource audio_backgroundMusic;

    // Start is called before the first frame update
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
        audio_backgroundMusic.Play();
        yield return null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
