using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour
{
    public float gravitySwitchTime = 20.0f;
    public float gravityTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityTimer = gravitySwitchTime;
    }

    // Update is called once per frame
    void Update()
    {
        gravityTimer -= Time.deltaTime;
        if (gravityTimer < 0)
        {
            Physics2D.gravity *= -1;
            gravityTimer = gravitySwitchTime;
        }
    }
}
