using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject powerBar;    
    private Rigidbody rb;
    private bool isPress;
    private bool isCheck;
    private Vector2 force;
    private float speed;
    private float rotSpeed;
    

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Last")
        {
            if(!isCheck)
            {
                gameManager.claim--;
                gameManager.winStrike = 0;
                gameManager.point -= 10;
                if(gameManager.point <= 0)
                {
                    gameManager.point = 0;
                }
                ReStart();
                Destroy(gameObject);
            }


        }
    }

    private void ReStart()
    {
        gameManager.ReCreate();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isCheck = false;
    }

    void Update()
    {
        gameManager.dif = Quaternion.Euler(0, transform.rotation.y+rotSpeed, 0) ;
        if(Input.GetMouseButtonDown(0))
        {
            force = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameManager.PowerBarStart();

            isPress = true;
           
        }

        if(Input.GetMouseButtonUp(0))
        {
            isPress = false;

            gameManager.PowerBarEnd();  
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            rb.AddForce(transform.forward * 100 * speed);
        }
    }

    void FixedUpdate()
    {
        if(isPress)
        {
            Vector3 a = Input.mousePosition;
            Vector2 b = new Vector2(a.x, a.y);
            Vector2 dir = b - force;


            rotSpeed = (-1 * dir.x) /10;
            float rbSpeed = Mathf.Abs(dir.y/100);

            speed = rbSpeed;
            gameManager.PowerBarValue(speed);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y+rotSpeed, transform.rotation.z);

        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Enemy")
        {
            isCheck = true;
            gameManager.claim++;

            GainPoint();
            Invoke("ReStart", 2f);
            Destroy(gameObject, 2f);
        }
    }

    private void GainPoint()
    {
        gameManager.point += 10;

        if(isCheck == true)
        {
           gameManager.winStrike++;
            gameManager.point += gameManager.winStrike * 2;
        }
        PlayerPrefs.SetInt("Point", gameManager.point);
    }



}
