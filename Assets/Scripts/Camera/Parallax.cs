using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float length, startpos;
    public GameObject myCamera;
    public float myParallaxEffect;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (myCamera.transform.position.x * (1 - myParallaxEffect));
        float dist = (myCamera.transform.position.x * myParallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length )
        {
            startpos += length;
        }
        else if(temp < startpos - length) 
        {
            startpos -= length;
        }
    }
}
