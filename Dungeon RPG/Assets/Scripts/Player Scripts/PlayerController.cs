using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerManager playerManager;
    // Values
    private float cooldown;

    void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    void Start()
    {

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            playerManager.isPotion = true;

        if(Input.GetKeyUp(KeyCode.LeftControl))
            playerManager.isPotion = false;

        PlayerMove();
        Skill();
        Attack();
    }

    private void PlayerMove()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            playerManager.MoveJump();
        }

        if(Input.GetKey(KeyCode.S))
        {
            playerManager.MoveBow();
        }

        if(Input.GetKey(KeyCode.A))
        {
            playerManager.isMove = true;
            playerManager.MoveLeft();

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
               playerManager.Dash(-1);
            }
        }

        if(Input.GetKey(KeyCode.D))
        {
            playerManager.isMove = true;
            playerManager.MoveRight();

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
               playerManager.Dash(1);
            }
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerManager.isMove = false;
        }           
    }

    private void Skill()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            playerManager.SwitchWeapon();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerManager.Skill(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerManager.Skill(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerManager.Skill(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerManager.Skill(4);
        }
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           playerManager.Attack();
        }
    }


}
