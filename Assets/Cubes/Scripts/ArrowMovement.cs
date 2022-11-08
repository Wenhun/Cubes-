using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] bool Up, Down, Left, Right;
    [SerializeField] float speed = 0.005f;
    [SerializeField] float distance = 0.04f;

    float x, y, z;
    float direction = -1f;

    private void OnEnable() 
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;
    }

    void Update()
    {
        if(Left)
        {
            transform.Translate(0 , 0 , direction * speed);
            if (transform.position.x < (x - distance))
                direction = direction * -1;
            if (transform.position.x > (x + distance))
                direction = direction * -1;
        }
        if(Right)
        {
            transform.Translate(0 , 0 , -direction * speed);
            if (transform.position.x < (x - distance))
                direction = direction * -1;
            if (transform.position.x > (x + distance))
                direction = direction * -1;
        }
        if(Up)
        {
            transform.Translate(0, 0 , direction * speed);
            if (transform.position.y < (y - distance))
                direction = direction * -1;
            if (transform.position.y > (y + distance))
                direction = direction * -1;
        }
        if(Down)
        {
            transform.Translate(0, 0 , -direction * speed);
            if (transform.position.y < (y - distance))
                direction = direction * -1;
            if (transform.position.y > (y + distance))
                direction = direction * -1;
        }
        
    }
}
