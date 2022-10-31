using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuffs : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Pos")
        {
            Vector3 dir = col.gameObject.transform.position;
            dir.y += 0.2f;
            col.gameObject.transform.position = dir;
        }
    }
}
