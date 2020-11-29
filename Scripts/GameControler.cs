using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{

    private int waveCount = 1;
    private int score = 0;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI deadScoretext;
    public GameObject deadCanvas;


    public GameObject pauseCanvas;

    public float maxTimebetweenWaves;
    private float currTimeBetweenWaves;
    private int enemyCount;
    private EnemySpawnSystem enemySpawnSystem;
    bool waiting = true;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawnSystem = FindObjectOfType<EnemySpawnSystem>();
        enemyCount = enemySpawnSystem.maxEnemys;
        currTimeBetweenWaves = maxTimebetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyCount);
        if(currTimeBetweenWaves <= 0)
        {
            if (waiting)
            {
                nextWave();
                waveCount++;
            }


        }
        else
        {
            if(waiting)
            {
                currTimeBetweenWaves -= Time.deltaTime;
                waveText.text = currTimeBetweenWaves.ToString("F2");

            }
            
        }

        if(enemyCount <= 0)
        {
            currTimeBetweenWaves = maxTimebetweenWaves;
            enemyCount = enemySpawnSystem.maxEnemys;
            waiting = true;

        }


    }


    public void nextWave()
    {
        waveText.text = "Wave " + waveCount;
        if (waveCount % 2 == 0)
        {
            enemySpawnSystem.maxEnemys++;
            enemySpawnSystem.maxShieldEnemys++;
        }
        enemySpawnSystem.spawnEnemys();
        
        waiting = false;
        
    }

    public void reduceEnemyCount()
    {
        enemyCount--;
        score += 100;
        scoreText.text = "Score: " + score;
    }

    public void setHighScore()
    {
        if(PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
    }

    public void setDeadCanvas()
    {
        Time.timeScale = 0;
        deadCanvas.SetActive(true);
        deadScoretext.text = "current Highscore: " + PlayerPrefs.GetInt("score");
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void setPauseCanvas()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        
    }


    public void closePauseCanvas()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
       
    }

}
