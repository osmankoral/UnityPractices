using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            playerManager.isGroundCheck = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Ground")
        {
            playerManager.isGroundCheck = false;
        }
    }
}
