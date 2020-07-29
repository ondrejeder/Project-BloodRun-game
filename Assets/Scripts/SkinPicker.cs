using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPicker : MonoBehaviour
{
    public bool skinUnlocked;
    
    public Text skinText;
    public int skinPrice;

    [Header ("eg. WhiteSkinUnlocked")] public string PlayerPrefsUnlockedName;
    [Header ("Skin name (eg white skin)")] public string skinName;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickSkin()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsUnlockedName) == 1) skinUnlocked = true; // CHECK IF THE SKIN IS UNLOCKED
        else skinUnlocked = false;

        if (skinUnlocked)  // SKIN IS UNLOCKED
        {
            skinText.text = "Pick " + skinName + ".";
            PlayerPrefs.SetString("SkinPicked", skinName);

            Debug.Log("picked " + skinName);
        }
        else  // SKIN IS NOT UNLOCKED
        {


            if (PlayerPrefs.GetFloat("CoinsAmount") >= skinPrice) // PLAYER HAS ENOUGH COINS TO BUY THE SKIN 
            {
                float remainingCoins = PlayerPrefs.GetFloat("CoinsAmount") - skinPrice; // REMOVE AMOUNT OF COINS AND MAKE THE SKIN UNLOCKED
                PlayerPrefs.SetFloat("CoinsAmount", remainingCoins);

                skinText.text = "Enjoy your skin!";

                skinUnlocked = true;
                PlayerPrefs.SetInt(PlayerPrefsUnlockedName, 1);   // SAVING THE FACT THAT THE SKIN IS UNLOCKED
            }
            else // PLAYER HAS NOT ENOUGH COINS TO BUY THE SKIN 
            {
                skinText.text = "Skin is locked, you can buy it for " + skinPrice + "c.";
                //whiteSkinText.text = "You do not have enough coins to buy this skin.";
            }
        }

    }
}
