using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkillManager : MonoBehaviour
{
    private Rigidbody2D rb2d;

    PlayerManager playerManager;

    private int rotate;

    void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        if(!playerManager.isFaced)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }
    }
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {

        rb2d.velocity = new Vector2(0.1f * rotate, 0);
        rb2d.position += rb2d.velocity; 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            if(col.transform.position.x > transform.position.x)
                col.GetComponent<EnemyManager>().TakeDamage(10);
            else if(col.transform.position.x < transform.position.x)
                col.GetComponent<EnemyManager>().TakeDamage(10);

            Destroy(gameObject);
        }
    }
}
