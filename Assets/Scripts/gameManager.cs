using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] GameObject circlePref, colorSwitchTool, ball, click, startPnl, recordPnl, gameOverPnl;
    [SerializeField] TextMeshProUGUI scoreTxt, highScoreTxt, endHighScoreTxt, endScoreTxt;
    [SerializeField] Camera cam;
    [SerializeField] Color[] colors;

    public bool isOver;
    int score, highScore, volume;
    bool isStart, isRecord;

    ballCs ballScript;
    cameraCs camScript;
    AudioSource audioSource;

    private void Start()
    {
        ballScript = GameObject.Find("Ball").GetComponent<ballCs>();
        camScript = Camera.main.GetComponent<cameraCs>();
        audioSource = GetComponent<AudioSource>();

        highScore = PlayerPrefs.GetInt("HighScore");
        volume = PlayerPrefs.GetInt("Volume");

        score = 0;
        scoreTxt.text = score.ToString();
        highScoreTxt.text = highScore.ToString();

        ballScript.enabled = true;
        camScript.enabled = true;

        isStart = false;
        isRecord = false;

        InvokeRepeating("SwitchCameraColor", 0, 5);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStart = true;
        }
        if (!isStart)
        {
            click.SetActive(true);
            startPnl.SetActive(true);
            recordPnl.SetActive(false);
            gameOverPnl.SetActive(false);
            scoreTxt.enabled = false;
        }
        else if(isStart && !isOver)
        {
            click.SetActive(false);
            startPnl.SetActive(false);
            highScoreTxt.enabled = false;
            scoreTxt.enabled = true;
        }

        if (isOver)
        {
            ballScript.enabled = false;
            camScript.enabled = false;
            scoreTxt.enabled = false;
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                endHighScoreTxt.text = highScore.ToString();
                recordPnl.SetActive(true);
                isRecord = true;
            }
            else if(score<=highScore && !isRecord)
            {
                gameOverPnl.SetActive(true);
                endScoreTxt.text = score.ToString();
            }
        }

        if (volume == 0)
        {
            audioSource.volume = 0.5f;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
    public void SpawnCircle()
    {
        GameObject circle = Instantiate(circlePref, new Vector2(ball.transform.position.x, ball.transform.position.y + 9f), Quaternion.Euler(0,0,Random.Range(0,360))) as GameObject;
        if(score < 1500)
        {
            circle.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            circle.GetComponent<circleCs>().turnSeed = 100;
            ballScript.jumpForce = 8.5f;
        }
        else if (score < 6500)
        {
            circle.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            circle.GetComponent<circleCs>().turnSeed = 115;
            ballScript.jumpForce = 7.5f;
        }
        else 
        {
            circle.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            circle.GetComponent<circleCs>().turnSeed = 125;
            ballScript.jumpForce = 7f;
        }
        Instantiate(colorSwitchTool, new Vector2(ball.transform.position.x, ball.transform.position.y + 13f), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    public void IncreaseScore()
    {
        if (score < 1500)
        {
            score += 100;
            scoreTxt.text = score.ToString();
        }
        else if (score < 6500)
        {
            score += 500;
            scoreTxt.text = score.ToString();
        }
        else
        {
            score += 1000;
            scoreTxt.text = score.ToString();
        }
      
    }

    void SwitchCameraColor()
    {
        int random = Random.Range(0, 4);
        cam.backgroundColor = Color.Lerp(cam.backgroundColor, colors[random], 15f*Time.deltaTime);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Volume()
    {
        if (volume == 0)
        {
            volume = 1;
            PlayerPrefs.SetInt("Volume", volume);
        }
        else
        {
            volume = 0;
            PlayerPrefs.SetInt("Volume", volume);
        }

    }


}
