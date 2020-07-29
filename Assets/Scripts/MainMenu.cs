using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text whiteSkinText;
    private bool whiteSkinUnlocked = false;
    public int whiteSkinPrice;

    public Text coinsText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
            
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + PlayerPrefs.GetFloat("CoinsAmount");



    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PickWhiteSkin()
    {
        if (PlayerPrefs.GetInt("WhiteSkinUnlocked") == 1) whiteSkinUnlocked = true; // CHECK IF THE SKIN IS UNLOCKED
        else whiteSkinUnlocked = false;

        if (whiteSkinUnlocked)  // SKIN IS UNLOCKED
        {
            whiteSkinText.text = "Pick white skin.";
            PlayerPrefs.SetString("SkinPicked", "white skin");

            Debug.Log("picked white skin");
        }
        else  // SKIN IS NOT UNLOCKED
        {
            

            if (PlayerPrefs.GetFloat("CoinsAmount") >= whiteSkinPrice) // PLAYER HAS ENOUGH COINS TO BUY THE SKIN 
            {
                float remainingCoins = PlayerPrefs.GetFloat("CoinsAmount") - whiteSkinPrice; // REMOVE AMOUNT OF COINS AND MAKE THE SKIN UNLOCKED
                PlayerPrefs.SetFloat("CoinsAmount", remainingCoins);

                whiteSkinText.text = "Enjoy your skin!";

                whiteSkinUnlocked = true;
                PlayerPrefs.SetInt("WhiteSkinUnlocked", 1);   // SAVING THE FACT THAT THE SKIN IS UNLOCKED
            }
            else // PLAYER HAS NOT ENOUGH COINS TO BUY THE SKIN 
            {
                whiteSkinText.text = "Skin is locked, you can buy it for " + whiteSkinPrice + "c.";
                //whiteSkinText.text = "You do not have enough coins to buy this skin.";
            }
        }
        
    }
    public void PickBlackSkin()
    {
        
        PlayerPrefs.SetString("SkinPicked", "black skin");

        Debug.Log("picked black skin");
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetFloat("Highscore", 0f);
    }

    public void ResetCoins()
    {
        PlayerPrefs.SetFloat("CoinsAmount", 0f);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetFloat("CoinsAmount", 0f); // RESETTING COINS
        PlayerPrefs.SetFloat("Highscore", 0f); // RESETTING HIGHSCORE
        PlayerPrefs.SetInt("WhiteSkinUnlocked", 0); // LOCKING WHITE SKIN
    }
}
