using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Monster monster;

    EnemyManagaer enemyManagaer;

    private void Awake()
    {
        enemyManagaer = GameObject.FindObjectOfType<EnemyManagaer>();
    }
    private void Start()
    {
       enemyManagaer.EnemyStat(monster.monsterName, monster.monsterHealth, monster.monsterMaxAttack, monster.monsterMinAttack, monster.monsterExp);
    }

}



