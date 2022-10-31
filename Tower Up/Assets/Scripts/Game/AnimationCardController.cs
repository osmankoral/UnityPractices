using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCardController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = rb.position;
        pos.x -= 0.05f;
        rb.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destroyer")
            Destroy(gameObject);
    }



}
