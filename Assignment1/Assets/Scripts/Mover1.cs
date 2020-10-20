using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover1 : MonoBehaviour
{
    private bool dirRight = true;
    //public float speed = 1000.0f;
    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * 5 * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * 5 * Time.deltaTime);

        if (transform.position.x >= 10.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -10.0f)
        {
            dirRight = true;
        }
    }
}
