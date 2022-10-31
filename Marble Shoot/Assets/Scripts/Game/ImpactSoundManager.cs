using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SoundOff", 0.5f);
    }

    private void SoundOff()
    {
        Destroy(gameObject);
    }
}
