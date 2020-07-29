using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bullet;
    public Transform firePoint;
    private float fireCounter;
    public float fireCounterValue;
    
    
    // Start is called before the first frame update
    void Start()
    {
        fireCounter = fireCounterValue;
    }

    // Update is called once per frame
    void Update()
    {
        fireCounter -= Time.deltaTime;
        if(fireCounter <= 0)
        {
            Fire();
            //Debug.Log("EnemyFire");
            fireCounter = fireCounterValue;
        }
    }

    public void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        
    }
}
