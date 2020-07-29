using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBullet : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float bulletSpeed;
    private bool deflected;
    private GameObject player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!deflected)
        {
            theRB.velocity = Vector2.left * bulletSpeed;
        }
        if(deflected)
        {
            theRB.velocity = Vector2.right * bulletSpeed * 1.3f;
        }
        

        if(gameObject.transform.position.x < (player.transform.position.x - 15) || gameObject.transform.position.x > (player.transform.position.x + 30))
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") || other.CompareTag("Enemy"))
    //    {
    //        gameObject.SetActive(false); 
    //    }
        
    //    if(other.CompareTag("Sword"))
    //    {
    //        deflected = true;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }

        if (other.collider.CompareTag("Sword"))
        {
            deflected = true;
        }
    }
}
