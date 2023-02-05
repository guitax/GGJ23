using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class DeathSceneManager : MonoBehaviour
{
    public AudioSource death_Sound;
    // Start is called before the first frame update

    private void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}