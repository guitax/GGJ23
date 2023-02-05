using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class DeathSceneManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource death_Sound;

    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}