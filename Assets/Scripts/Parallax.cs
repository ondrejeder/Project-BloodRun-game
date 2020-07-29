using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject gameCam;
    public float parallaxEffect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void FixedUpdate()
    {
        float temp = (gameCam.transform.position.x * (1 - parallaxEffect));
        float distance = (gameCam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += 2*length ;

        else if (temp < startPos - length) startPos -= length;
    }
}
