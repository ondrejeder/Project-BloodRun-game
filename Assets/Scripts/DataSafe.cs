using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSafe : MonoBehaviour
{
    public static DataSafe instance;

    public bool whiteSkinSelected, blackSkinSelected;

    public float coinsEarned;


    private void Awake()
    {
        instance = this;


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    
}
