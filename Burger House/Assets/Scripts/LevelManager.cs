using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private Transform burgerBranch;



    public void Level1On()
    {
        level2.SetActive(false);
        level1.SetActive(true);
    }


    public void Level2On()
    {
        level1.SetActive(false);
        level2.SetActive(true);
        for(int i=0; i<burgerBranch.childCount; i++)
        {
            Destroy(burgerBranch.GetChild(i).gameObject);
        }
    }

}
