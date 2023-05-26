using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    [SerializeField] TMPro.TextMeshProUGUI currentScoreText;
    public int currentScore;
    public int currentHighScore;
    public int allTimeHighscore;
    void Awake()
    {
        Instance = this;
        allTimeHighscore = PlayerPrefs.GetInt("highscore", 0);
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.State.MENU)
        {
            currentScore = 0;
        }
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            currentScoreText.text = "Score: " + currentScore;
        }
        if (GameManager.Instance.GameState == GameManager.State.GAMEOVER)
        {
            currentHighScore = currentScore;
            if(currentHighScore > allTimeHighscore){
                PlayerPrefs.SetInt("highscore", currentHighScore);
                allTimeHighscore = currentHighScore;
            }
        }
    }
}
