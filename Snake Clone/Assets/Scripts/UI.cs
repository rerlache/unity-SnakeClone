using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public SnakeLogic snake;
    public static UI Instance { get; private set; }
    public TMPro.TextMeshProUGUI _scoreText;
    public TMPro.TextMeshProUGUI _snakeLengthText;
    public TMPro.TextMeshProUGUI _gameOverText;
    public TMPro.TextMeshProUGUI _highScoreValue;
    int allTimeHighscore;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.State.HIGHSCORE)
        {
            _highScoreValue.text = Score.Instance.currentHighScore > allTimeHighscore ? Score.Instance.currentHighScore.ToString() : allTimeHighscore.ToString();
        }
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            _snakeLengthText.text = "Length: " + snake.body.Count.ToString();
        }
        if (GameManager.Instance.GameState == GameManager.State.GAMEOVER)
        {
            allTimeHighscore = PlayerPrefs.GetInt("highscore", 0);
            if (Score.Instance.currentScore > allTimeHighscore)
            {
                _gameOverText.text = string.Format("CONGRATS!<br><br>{0}<br>is your new Highscore!", Score.Instance.currentScore);
            }
            else
            {
                _gameOverText.text = string.Format("Your final score: {0}<br>Your Highscore: {1}", Score.Instance.currentHighScore, Score.Instance.allTimeHighscore);
            }
        }
    }

    #region Button Functions
    public void StartButtonClicked() { GameManager.Instance.SwitchState(GameManager.State.PLAY); }
    public void InstructionButtonClicked() { GameManager.Instance.SwitchState(GameManager.State.INSTRUCTIONS); }
    public void BackToMainmenuButtonClicked() { GameManager.Instance.SwitchState(GameManager.State.MENU); }
    public void HighScoreButtonClicked() { GameManager.Instance.SwitchState(GameManager.State.HIGHSCORE); }
    #endregion
}
