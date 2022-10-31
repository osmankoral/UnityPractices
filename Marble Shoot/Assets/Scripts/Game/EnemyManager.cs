using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject impactPref;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player")
        {
            Instantiate(impactPref, transform);
            gameManager.index--;
            Destroy(gameObject, 2f);
        }
    }
}
