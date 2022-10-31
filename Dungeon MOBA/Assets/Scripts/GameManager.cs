using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Transform enemyTransform2;
    [SerializeField] private Transform enemyTransform3;

    private GameObject player;
    private GameObject enemy;
    private GameObject enemy2;
    private GameObject enemy3;
    private Camera mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        //player = Instantiate(playerPref, playerTransform.position, playerTransform.rotation);
        enemy = Instantiate(enemyPref, enemyTransform.position, enemyTransform.rotation);
        enemy2 = Instantiate(enemyPref, enemyTransform2.position, enemyTransform2.rotation);
        enemy3 = Instantiate(enemyPref, enemyTransform3.position, enemyTransform3.rotation);

    }
}
