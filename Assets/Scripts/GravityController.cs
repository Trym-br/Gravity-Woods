using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using TMPro;

public class GravityController : MonoBehaviour
{
    public float gravitySwitchTime = 20.0f;
    public float gravityTimer = 0f;

    public AudioClip gravitySwitchSound;
    private AudioSource _audioSource;

    public TMP_Text gravityChangeText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityTimer = gravitySwitchTime;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gravityTimer -= Time.deltaTime;
        gravityChangeText.text = Math.Round(gravityTimer).ToString();
        if (gravityTimer < 0)
        {
            Physics2D.gravity *= -1;
            gravityTimer = gravitySwitchTime;
            _audioSource.PlayOneShot(gravitySwitchSound);
        }
    }
}
