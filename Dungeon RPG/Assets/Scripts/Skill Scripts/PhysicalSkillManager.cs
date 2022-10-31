using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSkillManager : MonoBehaviour
{
    void Start()
    {

    }

    public void Dash(Rigidbody2D rb, int x, int lvl)
    {
        switch (lvl)
        {
            case 0:
                Dash(rb, x);
                break;
            case 1:
                Dash1(rb, x);
                break;
            case 2:
                Dash2(rb, x);
                break;
            default:
                Dash2(rb, x);
                break;

        }
    }

    private void Dash(Rigidbody2D rb, float x)
    {
        rb.position += new Vector2(0.5f * x, 0);
    }

    private void Dash1(Rigidbody2D rb, float x)
    {
        rb.position += new Vector2(1f * x, 0);
    }

    private void Dash2(Rigidbody2D rb, float x)
    {
        rb.position += new Vector2(1f * x, 0);
    }
}
