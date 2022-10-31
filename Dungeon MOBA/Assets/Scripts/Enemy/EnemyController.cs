using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private GameObject player;
    float rotateVelocity;
    float enemyDistance;
    Animation anim;
    float attackCoolDown;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animation>();
    } 
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        
        enemyDistance = Vector3.Distance(player.transform.position, transform.position);

        attackCoolDown += Time.deltaTime;
        // Enemy Look Rotation
        EnemyLookRotation();

        //Enemy Movement
        if(enemyDistance > 2f) EnemyMove();
        else EnemyAttack();
        
    }

    private void EnemyLookRotation()
    {
        Quaternion rotationLook = Quaternion.LookRotation(player.transform.position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLook.eulerAngles.y, ref rotateVelocity, 0.1f * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    private void EnemyMove()
    {
        if(enemyDistance < 8)
        {
            transform.GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, player.transform.position, 0.1f);
            anim.Play("run");
        }
        

    }
    
    private void EnemyAttack()
    {
        if(attackCoolDown >= 2f)
        {
            anim.Play("attack1");
            attackCoolDown = 0;
        }
    }
}
