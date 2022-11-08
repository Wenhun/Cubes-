using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetShake : MonoBehaviour
{
    SwapCubes swapCubes;
    Vector3 rotate;

    public float toggleTime = 0.5f; // change direction every 0.2s
    public float maxAngle = 1;

    public bool horizontal;
    public bool vertical;

    [SerializeField] GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        swapCubes = FindObjectOfType<SwapCubes>();
        StartCoroutine(ShakeObject());
        cube = gameObject;
        rotate = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(swapCubes.cube1 == gameObject)
        {
            StopAllCoroutines();
            transform.eulerAngles = rotate;
        }
        else
            StartCoroutine(ShakeObject());
        if(swapCubes.cube2 == gameObject)
        {
            StopAllCoroutines();
            transform.eulerAngles = rotate;
        }
        else
            StartCoroutine(ShakeObject());
    }

    public IEnumerator ShakeObject() 
    {
        int direction = 1;
        float timer = 0;
        while (true) 
        {
            timer += Time.deltaTime;
            float zAngle = direction * timer * (maxAngle / toggleTime);
            if (horizontal)
                transform.rotation = Quaternion.Euler(zAngle, 0, 0);
            if (vertical)
                transform.rotation = Quaternion.Euler(0, zAngle, -90);    
            if (timer >= toggleTime) 
            {
                direction = direction * -1;
                timer = 0;
            }
            yield return null;
        }
    }
}
