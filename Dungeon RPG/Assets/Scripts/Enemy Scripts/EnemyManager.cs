using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private Slider healtBar;

    ItemManager itemManager;

    private float maxHealth = 100;
    private float health;
    private float speed = 3f;
    private bool isFaced;
    private Vector3 startPosition;
    private int turnNumber;
    private bool reTurn;
    private float disEnemy;
    private float attackRange = 1f;
    private float inArea;
    private float attackSpeed;
    private float healthBarTime;
    private bool isHealthBar;
    private int isDead;

    void Awake()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        healtBar = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        health = maxHealth;
        healtBar.maxValue = maxHealth;
        healtBar.value = health; 
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Dead();
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Move();
            Attack();
        }

        if(isHealthBar)
        {
            healthBarTime -= Time.deltaTime;
            if(healthBarTime <= 0)
            {
                healtBar.gameObject.SetActive(false);
                isHealthBar = false;
            }
        }

    }

    private void Move()
    {
        Vector3 playerPoz = player.transform.position;
        Vector3 myPoz = transform.position;
    
        float disEnemyCol = player.GetComponent<BoxCollider2D>().size.x/2;
        float disMyCol = gameObject.GetComponent<BoxCollider2D>().size.x/2;
        disEnemy = Mathf.Abs(myPoz.x - playerPoz.x) - disEnemyCol - disMyCol;

        if (disEnemy < 5)
        {
            MoveOnPlayer(playerPoz.x, myPoz.x, disEnemy);
        }
        else
        {
            SelfMove();
        }

    }

    private void MoveOnPlayer(float playerPoz_x, float myPoz_x, float dis)
    {
        if (playerPoz_x < myPoz_x && dis > attackRange)
        {
            LeftMove();
        }
        else if (playerPoz_x > myPoz_x && dis > attackRange)
        {
            RightMove();
        }
        else
        {
           // animator.SetBool("isMove", false);
        }
    }

    private void SelfMove()
    {

        float dis = transform.position.x - startPosition.x;
        if (!reTurn)
        {
            RightMove();
            if (dis > 3)
                reTurn = !reTurn;
            
        }
        if(reTurn)
        {
            LeftMove();
            if (dis < -3)
                reTurn = !reTurn;
        }



    }

    private void RightMove()
    {
        
        if (isFaced)
        {
            Quaternion dir = transform.rotation;
            dir.y = 0;
            transform.rotation = dir;

            isFaced = !isFaced;
        }
        animator.SetBool("isMove", true);
        Vector2 a = rb.position;
        a.x += speed * Time.deltaTime;
        rb.position = a;
        StartCoroutine(DelayMove());
    }
    private void LeftMove()
    {
        
        if (!isFaced)
        {
            Quaternion dir = transform.rotation;
            dir.y = 90;
            transform.rotation = dir;

            isFaced = !isFaced;
        }
        animator.SetBool("isMove", true);
        Vector2 a = rb.position;
        a.x -= speed * Time.deltaTime;
        rb.position = a;
        StartCoroutine(DelayMove());
    }
    IEnumerator DelayMove()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isMove", false);
    }

    private void Attack()
    {
        if(disEnemy <= attackRange)
        {
            if(inArea <= 1f)
                inArea += Time.deltaTime;

            attackSpeed -= Time.deltaTime;

            if(inArea >= 0.5f)
            {
                if(attackSpeed <= 0)
                {
                    animator.SetBool("isAttack", true);
                    player.GetComponent<PlayerManager>().TakeDamage(10);
                    attackSpeed = 1;
                    StartCoroutine(DelayAttack());
                }
            }
        }
        if(disEnemy > attackRange)
        {
            inArea = 0;
        }
    }
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isAttack", false);

    }

    public void TakeDamage(float damage, float force)
    {
        animator.SetBool("isHit", true);
        isHealthBar = true;
        healtBar.gameObject.SetActive(true);
        health -= damage;
        healtBar.value = health;
        healthBarTime = 3f;
        StartCoroutine(DelayHit());
        rb.AddForce(transform.right * 100f * force);  
    }
    public void TakeDamage(float damage)
    {
  
        animator.SetBool("isHit", true);
        isHealthBar = true;
        healtBar.gameObject.SetActive(true);
        health -= damage;
        healtBar.value = health;
        healthBarTime = 3f; 
        StartCoroutine(DelayHit());
    }   
    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isHit", false);
    }


    private void Dead()
    {
        if(isDead <1)
        {
            player.GetComponent<PlayerManager>().GainExp(5f);
            itemManager.ItemAdd();
            isDead = 1;
        }

        animator.SetBool("isDead", true);
        
        StartCoroutine(DelayDead());
    }
    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
