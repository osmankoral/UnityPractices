using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public void CameraMove(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
