using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER
    }
    public GameState currentGameState = GameState.GS_PAUSEMENU;
    public static GameManager instance;
    public Canvas pauseMenuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public Canvas levelCompleteCanvas;
    public Text coinText;
    public Text enemyKilledText;

    private int killedEnemies = 0;
    private int money = 0;
    private int eur = 0;
    private int gbp = 0;
    private int coins_left;
    private GameObject[] coinsEUR;
    private GameObject[] coinsGBP;
    private int number_of_keys = 0;
    private int number_of_keys_left = 0;
    private int lives_number = 3;
    private int scenesNumber;

    public Image[] boxes;
    public Image[] lives;
    public Image special;
    public Text notYet;
    public Text timerText;
    private float seconds =0;
    private float minutes = 0;
    private float timer = 0;

    private bool gameFullyCompleted = false;
    private string next_level_name = "";

    public int Number_of_keys_left { get => number_of_keys_left; set => number_of_keys_left = value; }
    public int Number_of_keys { get => number_of_keys; set => number_of_keys = value; }

    void Awake()
    {
        instance = this;
        InGame();
        coinsEUR = GameObject.FindGameObjectsWithTag("CoinEUR");
        coinsGBP = GameObject.FindGameObjectsWithTag("CoinGBP");
        scenesNumber = SceneManager.sceneCountInBuildSettings -1;
        coins_left = coinsEUR.Length + coinsGBP.Length;
        number_of_keys_left = GameObject.FindGameObjectsWithTag("Box").Length;
        coinText.text = string.Format("{0}", money);
        enemyKilledText.text = string.Format("{0}", killedEnemies);
        for (int i = 0; i < number_of_keys_left; i++)
        {
            boxes[i].color = Color.gray;
        }
        foreach (Image live in lives)
        {
            live.color = Color.white;
        }
        special.enabled = false;
        notYet.enabled = false;
        minutes = 0;
        seconds = 0;
        timerText.text = string.Format("czas: {0:00}:{1:00}", minutes, seconds);
        GetNextLevel(); 
    }

    void GetNextLevel()
    {
        string[] result = Regex.Split(SceneManager.GetActiveScene().name, "[a-z]+", RegexOptions.IgnoreCase);
        int next_level = Convert.ToInt32(result[1]);
        next_level += 1;
        Debug.LogWarning("Next lvl : " + next_level);
        Debug.Log("Levels: " + scenesNumber);
        if (next_level > scenesNumber)
        {
            gameFullyCompleted = true;
            //toDo canvas for gameCompleted
            
        }
        else
        {
            next_level_name = "Level" + Convert.ToString(next_level);
            Debug.LogWarning("Next lvl name: " + next_level_name);

        }

    }

    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);
        levelCompleteCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
    }
    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }
    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }
    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }
    public void LevelCompleted()
    {
        //save to file
        string path = SceneManager.GetActiveScene().name + ".stats";
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Coins: " + money);
                sw.WriteLine("Killed: " + killedEnemies);
                sw.WriteLine("Heart: " + lives_number);
                sw.WriteLine("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
            }
        }
        else
        {
            File.Delete(path);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Coins: " + money);
                sw.WriteLine("Killed: " + killedEnemies);
                sw.WriteLine("Heart: " + lives_number);
                sw.WriteLine("Time: " + string.Format("{0:00}:{1:00}", minutes, seconds));
            }
        }
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_PAUSEMENU)
        {
            InGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_GAME)
        {
            PauseMenu();
        }
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNextLvlButtonClicked()
    {
        if (!gameFullyCompleted)
        {
            SceneManager.LoadScene(next_level_name);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void AddCoin(int number)
    {
        money += number;
        coinText.text = string.Format("{0}", money);
    }

    public void AddKey()
    {
        boxes[number_of_keys].color = Color.white;
        number_of_keys += 1;
    }

    public void AddLive()
    {
        if(lives_number == 4)
        {
            return;
        }
        lives_number += 1;
        Debug.Log(lives_number);
        if (lives_number == 4)
        {
            special.enabled = true;
        }
        else
        {
            lives[lives_number - 1].enabled = true;
            lives[lives_number - 1].color = Color.white;
        }
    }
    public void TakeLife()
    {
        lives_number -= 1;
        if (lives_number <= 0)
        {
            lives[lives_number].color = Color.gray;
            instance.currentGameState = GameState.GS_GAME_OVER;
            GameOver();
        }
        else
        {
            Debug.Log(lives_number);
            if (lives_number == 3)
            {
                special.color = Color.gray;
            }
            else if(lives_number < 3)
            {
                lives[lives_number].enabled = false;
            }
        }
    }

    public void End(bool value)
    {
        LevelCompleted();
    }

    public void GiveMessageNotYet(bool value)
    {
        notYet.enabled = value;
    }

    public void AddToTime(float sec)
    {
        if(timer < 1.0f)
        {
            timer += sec;
        }
        else
        {
            timer = 0;
            if(seconds < 59)
            {
                seconds += 1;
            }
            else
            {
                seconds = 0;
                minutes += 1;
            }
        }
        timerText.text = string.Format("czas: {0:00}:{1:00}", minutes, seconds);

    }

    public void KillEnemy(int number)
    {
        killedEnemies += number;
        enemyKilledText.text = string.Format("{0}", killedEnemies);
    }
}
