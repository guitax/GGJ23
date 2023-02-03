using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    
    private Material currentTile;
    public float speed;
    private float offset;
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        this.renderer.material.mainTextureOffset = new Vector2 (0f, -Time.time * speed);
    }

}
