using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour
{
    public float gravitySwitchTime = 20.0f;
    public float gravityTimer = 0f;
    
    private PlayerController _playerController;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityTimer = gravitySwitchTime;
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); //Reference to the playercontroller script which lies within the player object.
    }

    // Update is called once per frame
    void Update()
    {
        gravityTimer -= Time.deltaTime;
        if (gravityTimer < 0)
        {
            Physics2D.gravity *= -1;
            gravityTimer = gravitySwitchTime;
            _playerController.isUpsideDown = !_playerController.isUpsideDown; // Sets the players upside down status to the opposite
        }
    }
}
