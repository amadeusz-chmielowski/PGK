using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System;

public class MainMenu : MonoBehaviour
{

    public Canvas menuCanvas;
    public Canvas statsCanvas;
    public Canvas levelsCanvas;
    public Text[] cointScore;
    public Text[] heartScore;
    public Text[] killScore;
    public Text[] timeScore;
    public Text[] Score;
    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.enabled = true;
        statsCanvas.enabled = false;
        levelsCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }

    public void OnLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Level1"));
    }

    public void OnLevel2ButtonPressed()
    {
        StartCoroutine(StartGame("Level2"));
    }

    public void OnLevel3ButtonPressed()
    {
        StartCoroutine(StartGame("Level3"));
    }

    public void OnLevel4ButtonPressed()
    {
        StartCoroutine(StartGame("Level4"));
    }

    public void OnStatsButtonPressed()
    {
        //toDo
        LoadLevelStats();
        menuCanvas.enabled = false;
        statsCanvas.enabled = true;
    }

    public void OnHighScorewButtonPressed()
    {
        //toDo
        LoadLevelHighScore();
        menuCanvas.enabled = false;
        statsCanvas.enabled = true;
    }

    public void OnLevelsButtonPressed()
    {
        menuCanvas.enabled = false;
        levelsCanvas.enabled = true;
    }

    public void OnBackButtonPressed()
    {
        //toDo
        menuCanvas.enabled = true;
        statsCanvas.enabled = false;
        levelsCanvas.enabled = false;
    }

    public void OnExitButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.LogWarning("Exiting unity application");
#else
        Application.Quit(0);
        Debug.LogWarning("Exiting standalone application");
#endif

    }

    public void LoadLevelStats()
    {
        Dictionary<string, int> paths = new Dictionary<string, int>()
        {
            { "Level1.stats" , 0},
            { "Level2.stats" , 1},
            { "Level3.stats" , 2},
            { "Level4.stats" , 3}
        };

        foreach (KeyValuePair<string, int> entry in paths)
        {
            if (File.Exists(entry.Key))
            {
                using (StreamReader sr = File.OpenText(entry.Key))
                {
                    cointScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    killScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    heartScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    timeScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    CalculateScore(entry.Value);
                }
            }
        }
    }

    public void LoadLevelHighScore()
    {
        Dictionary<string, int> paths = new Dictionary<string, int>()
        {
            { "Level1.high" , 0},
            { "Level2.high" , 1},
            { "Level3.high" , 2},
            { "Level4.high" , 3}
        };

        foreach (KeyValuePair<string, int> entry in paths)
        {
            if (File.Exists(entry.Key))
            {
                using (StreamReader sr = File.OpenText(entry.Key))
                {
                    cointScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    killScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    heartScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    timeScore[entry.Value].text = sr.ReadLine().Split(' ')[1];
                    CalculateScore(entry.Value);
                }
            }
        }
    }

    public void CalculateScore(int index)
    {
        int score = 0;
        Debug.LogWarning(Convert.ToInt32(timeScore[index].text.Split(':')[0]));
        score = 5 * ((1 + Convert.ToInt32(cointScore[index].text)) * (1 + Convert.ToInt32(killScore[index].text)) * (1 + Convert.ToInt32(heartScore[index].text))) - ((360 * Convert.ToInt32(timeScore[index].text.Split(':')[0])) + (1 * Convert.ToInt32(timeScore[index].text.Split(':')[1])));
        Score[index].text = "Score: " + Convert.ToString(score);
    }

}
