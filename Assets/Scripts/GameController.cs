using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public AudioClip winMusic;
    public AudioClip backMusic;
    public AudioClip loseMusic;
    public AudioSource musicSource;
    public bool win;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text creditsText;
    private int score;

    private bool gameOver;
    private bool restart;

    

    void Start()
    {
        gameOver = false;
        gameOverText.text = "";
        restart = false;
        restartText.text = "";
        win = false;
        winText.text = "";
        creditsText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        
    }

    private void Awake()
    {
        musicSource.clip = backMusic;
        musicSource.Play();
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.L))
            {
                SceneManager.LoadScene("Main");
            }
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'L' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score = score + newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!";
            creditsText.text = "Game Created By: Joshua Cothern";
            gameOver = true;
            restart = true;
            win = true;

            musicSource.clip = winMusic;
            musicSource.Play();


        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = loseMusic;
        musicSource.Play();
    }

}