using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform startPosition;

    [SerializeField] private GameObject EnemyPref;
    [SerializeField] private Transform EnemyPosition;

    // Scriptable Object
    [SerializeField] private PlayerStatScriptableObject playerStat;
    [SerializeField] private SkillDatabaseObject skillDb;

    // Scripts Referances
    CameraControl cameraControl;
    ItemManager itemManager;
    
    public GameObject player;
    private GameObject enemy;

    void Awake()
    {
        cameraControl = GameObject.FindObjectOfType<CameraControl>();
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }
    void Start()
    {
        player = Instantiate(playerPref, startPosition.position, startPosition.rotation); 
        player.GetComponent<PlayerManager>().startPos = startPosition;

        enemy = Instantiate(EnemyPref, EnemyPosition.GetChild(0).position, EnemyPosition.GetChild(0).rotation);
    }

    // Update is called once per frame
    void Update()
    {   
        //Camera Control
        if(cameraControl != null)
        {
            Vector3 dir = player.transform.position;
            dir.y += 2.07f;
            dir.z = -10f;
            cameraControl.CameraMove(dir.x, dir.y, dir.z);
        }
        if(player != null)
        {

        }
        if(enemy == null)
        {
            int a = Random.Range(0, EnemyPosition.childCount);
            enemy = Instantiate(EnemyPref, EnemyPosition.GetChild(a).position, EnemyPosition.GetChild(a).rotation);
        }


    }
 
}





