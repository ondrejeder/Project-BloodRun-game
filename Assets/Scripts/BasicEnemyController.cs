using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public bool jumping;
    public Rigidbody2D theRB;
    public float jumpForce;
    public ParticleSystem deathparticle;
    [Header("Points to add per killed enemy")]public float pointPerEnemy;
    
    
    
    void Start()
    {
        int jumpYoN = Random.Range(0, 2);
        switch(jumpYoN)
        {
            case 0:
                jumping = false;
                break;
            case 1:
                jumping = true;
                break;
        }

        if (jumping)
        {
            StartCoroutine("JumpCor");
        }
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Sword") || other.collider.CompareTag("Bullet"))
        {
            //Debug.Log("EnemyHit");
            Destroy(gameObject);
            Instantiate(deathparticle, gameObject.transform.position, gameObject.transform.rotation);

            GameManager.instance.scorePoints += pointPerEnemy;

            if (other.collider.CompareTag("Sword"))
            {
                PlayerController.instance.killedEnemy = true;

                Debug.Log("ENEMY DEAD");
            }
        }
    }

    public void Jump()
    {
        theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator JumpCor()
    {
        Jump();
        yield return new WaitForSeconds(Random.Range(2, 5));

        StartCoroutine("JumpCor");

    }

    
}
