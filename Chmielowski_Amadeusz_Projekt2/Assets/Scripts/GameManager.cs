using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
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

    public Image[] boxes;
    public Image[] lives;
    public Image special;
    public Image gameOver;
    public Image lvlComplete;
    public Text notYet;
    public Text timerText;
    private float seconds =0;
    private float minutes = 0;
    private float timer = 0;

    public int Number_of_keys_left { get => number_of_keys_left; set => number_of_keys_left = value; }
    public int Number_of_keys { get => number_of_keys; set => number_of_keys = value; }

    void Awake()
    {
        instance = this;
        coinsEUR = GameObject.FindGameObjectsWithTag("CoinEUR");
        coinsGBP = GameObject.FindGameObjectsWithTag("CoinGBP");
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
        gameOver.enabled = false;
        lvlComplete.enabled = false;
        notYet.enabled = false;
        minutes = 0;
        seconds = 0;
        timerText.text = string.Format("czas: {0:00}:{1:00}", minutes, seconds);


    }

    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (newGameState == GameState.GS_GAME);
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
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.currentGameState == GameState.GS_PAUSEMENU)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                instance.InGame();
            }
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
        lives_number += 1;
        Debug.Log(lives_number);
        if (lives_number >= 4)
        {
            lives_number = 4;
        }
        if (lives_number >= 3)
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
            gameOver.enabled = true;
        }
        else
        {
            Debug.Log(lives_number);
            if (lives_number >= 3)
            {
                special.color = Color.gray;
            }
            else
            {
                lives[lives_number].enabled = false;
            }
        }
    }

    public void End(bool value)
    {
        lvlComplete.enabled = value;
    }

    public void GiveMessageNotYet(bool value)
    {
        notYet.enabled = value;
    }

    public void AddToTime(float sec)
    {
        Debug.Log(sec);
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
