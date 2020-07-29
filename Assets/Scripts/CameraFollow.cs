using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform targetPlayer;
    public float xMin, xMax;
    public float yMin, yMax;

    //public bool zoomOut;
    public float zoomSpeed;
    public float zoomedOutSize;
    public float zoomedInSize;
    public float zoomedInSlashSize;
    public float yPlus = 1;


    public Camera camera;




    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if (!targetPlayer)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }

        

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(targetPlayer.position.x + 7.5f, xMin, xMax), Mathf.Clamp(targetPlayer.position.y + yPlus, yMin, yMax), transform.position.z);   // pos.x + 8  and pos.y + 2 change pos of camera so player is not in center

        if(PlayerController.instance.zoomOut)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomedOutSize, zoomSpeed);
            yPlus = -1;
        }
        if (PlayerController.instance.canNextDash)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomedInSlashSize, zoomSpeed * 0.5f);
            yPlus = -4;
        }
        if (!PlayerController.instance.zoomOut)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomedInSize, zoomSpeed);
            yPlus = 1;
        }


    }


}
