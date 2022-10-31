using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    private Rigidbody rb;
    private float jumpX;
    private float jumpZ;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        CubeMove();
    }

    private void CubeMove()
    {
        rb.AddForce(0, 0, 20, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Finish")
        {
            Debug.Log("Game Over !!");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        List<GameObject> ab = new List<GameObject>();
        if (col.gameObject.tag == gameObject.tag)
        {
            ab.Add(col.gameObject);
            int index = 0;
            switch (col.gameObject.tag)
            {
                case "Cube2"   : index = 1;  break;
                case "Cube4"   : index = 2;  break;
                case "Cube8"   : index = 3;  break;
                case "Cube16"  : index = 4;  break;
                case "Cube32"  : index = 5;  break;
                case "Cube64"  : index = 6;  break;
                case "Cube128" : index = 7;  break;
                case "Cube256" : index = 8;  break;
                case "Cube512" : index = 9;  break;
                case "Cube1024": index = 10; break;
            }

            Vector3 a = col.transform.position;
            Vector3 b = transform.position;

            if (a.z < b.z) gameManager.a_ = b;
            else if (b.z < a.z) gameManager.a_ = a;

            gameManager.index_ = index;

            gameManager.isMerge = true;

            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }

    public void Jump()
    {
        JumpCode();
    }
    private void JumpCode()
    {
        FindJumpPoz();
        rb.AddForce(jumpX, 8, jumpZ, ForceMode.Impulse);
    }

    private void FindJumpPoz()
    {
        string amI = gameObject.tag;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(amI);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (var enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemy != gameManager.cube)
            {

                if (DistanceToEnemy < shortesDistance && enemy != gameObject)
                {
                    shortesDistance = DistanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy == null){ jumpX = 0; jumpZ = 0; return; }
        Vector3 targetPoz = nearestEnemy.transform.position;
        Vector3 myPoz = transform.position;

        jumpX = targetPoz.x - myPoz.x;
        jumpZ = targetPoz.z - myPoz.z;
        
        if (jumpX > 2 || jumpZ > 2) { jumpX = 2; jumpZ = 2; }
        if (jumpX < -2 || jumpZ < -2) { jumpX = -2; jumpZ = -2; }
    }
}
