using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public Transform genPoint;
    public GameObject platformToCreate;
    private GameObject targetEndPoint;
    public GameObject startPlatform;
    public Transform endPointStart;
    [HideInInspector]
    public float dif;
    private GameObject newPlatform;
    [Header("Platforms to pick from")]
    public List<GameObject> platforms;
    [Header("Enemies to pick from")]
    public List<GameObject> enemies;
    private GameObject selectedPlatform;
    public Transform selectedPlatformEndPoint;
    private bool shouldGenerate;
    private bool generateEnemy;
    [Header("How much the platform can be moved on y axis")]
    public float yHeightGenPlatformOffset;

    
    
    void Start()
    {
        shouldGenerate = true;

    }

    
    void Update()
    {
        if(!targetEndPoint)
        {
            FindNewTargetPoint();
        }

        if (!newPlatform)
        {
            newPlatform = startPlatform;
        }

        // GENERATION
        if (genPoint.position.x > targetEndPoint.transform.position.x && shouldGenerate)
        {
            //Debug.Log("Generate");
            SelectPlatformAndDifference();

            Instantiate(selectedPlatform, targetEndPoint.transform.position + new Vector3(dif, Random.Range(-yHeightGenPlatformOffset, yHeightGenPlatformOffset), 0), genPoint.rotation);

            

            shouldGenerate = false;

            FindNewTargetPoint();

            FindNewPlatform();

            // GENERATE ENEMY
            float chanceToGenEnemy = Random.Range(0, 1f);
            if (chanceToGenEnemy < .66f) generateEnemy = true;
            else generateEnemy = false;

        }

        // ENEMY GENERATION
        if(generateEnemy && selectedPlatform.CompareTag("Platform"))
        {
            GameObject selecteEnemy = enemies[Random.Range(0, enemies.Count)];

            Instantiate(selecteEnemy, newPlatform.transform.position + new Vector3(Random.Range(-3,4),2,0), genPoint.rotation);
            
            generateEnemy = false;

            
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void FindNewTargetPoint()   // find EndPoint that has x greater than x of GenPoint - switch endpoint to be in front of GenPoint
    {
        GameObject[] endPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        foreach (var endPoint in endPoints)
        {
            if(endPoint.transform.position.x > genPoint.position.x)
            {
                targetEndPoint = endPoint;
            }
        }

        shouldGenerate = true;   // found new EndPoint -> enable generation
    }

    public void SelectPlatformAndDifference()  //select which platform to generate
    {
        selectedPlatform = platforms[Random.Range(0,platforms.Count)];      // select platforms from prefabs
        selectedPlatformEndPoint = selectedPlatform.transform.GetChild(0).transform;    // gets endpoint of selected platform in prefab

        dif = selectedPlatformEndPoint.transform.position.x - selectedPlatform.transform.position.x;   // endpoint(in prefab) - selectedplatform(in prefab)
    }


    public void FindNewPlatform()   // selects new platform in scene (not prefab)
    {
        GameObject[] targetPlatforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var targetPlatofrm in targetPlatforms)
        {
            if (targetPlatofrm.transform.position.x > genPoint.position.x)
            {
                newPlatform = targetPlatofrm;
            }
            
        }
    }


}
