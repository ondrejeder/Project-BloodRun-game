using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float scorePoints = 0;
    public float highscore = 0;
    public float scorePointPerSecond;
    public bool increaseScore;

    public Text scoreText, highScoreText;
    public Text coinsText;
    public Text youDieText, youEarnText;

    public Button restartButton;
    public GameObject PausePanelUI;

    public bool gamePaused = false;

    public float coinsEarned;
    public float coinsAmount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        increaseScore = true;
        Time.timeScale = 1;

        
        Application.targetFrameRate = 60;

        highscore = PlayerPrefs.GetFloat("Highscore");
        highScoreText.text = "Highscore: " + highscore;

        
    }




    void Update()
    {
        

        // ADDING SCORE
        if (increaseScore)
        {
            scorePoints += scorePointPerSecond * Time.deltaTime;
        }

        scoreText.text = "Score: " + Mathf.Round(scorePoints);
        
        coinsText.text = "Coins: " + PlayerPrefs.GetFloat("CoinsAmount");

        coinsEarned = Mathf.Round(scorePoints * 0.1f);

        // just for debugging
        youEarnText.text = "You have earned " + coinsEarned + " coins.";
        youDieText.text = "Your score: " + Mathf.Round(scorePoints);

        if(gamePaused)
        {
            Time.timeScale = 0;
        }

        if(!gamePaused)
        {
           if(PlayerController.instance.canNextDash && PlayerController.instance.killedEnemy)
            {
                Time.timeScale = 0.75f;
                //StartCoroutine("SlowTimeInSlash");
            }
            else
            {
                Time.timeScale = 1;
            }

        }

        if(scorePoints > highscore)
        {
            highscore = Mathf.Round(scorePoints);
            PlayerPrefs.SetFloat("Highscore", highscore);
            highScoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");

        AddAndSaveCoins();
    }

    public void PauseGame()
    {
        gamePaused = true;
        PausePanelUI.SetActive(true);


    }

    public void ResumeGame()
    {
        gamePaused = false;
        PausePanelUI.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");

        AddAndSaveCoins();
    }

    public IEnumerator SlowTimeInSlash()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;
    }

    public void AddAndSaveCoins()
    {
        coinsAmount = PlayerPrefs.GetFloat("CoinsAmount") + coinsEarned; // RESETTING COINS
        PlayerPrefs.SetFloat("CoinsAmount", coinsAmount); 
    }
}
