using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Running,
        Over,
    }

    private int _score;
    public int difficulty;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private Button _restartGameButton;
    [SerializeField]
    private GameObject _difficultyPanel;

    private GameState _state;
    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            OnStateChange?.Invoke(value);
        }
    }

    public event Action<GameState> OnStateChange;
    private void Start()
    {
        State = GameState.Start;
        difficulty = 1;
        OnStateChange += (state) =>
        {
            _difficultyPanel.gameObject.SetActive(state == GameState.Start);
        };
    }

    public void StartGame(int difficultyRate)
    {
        difficulty = difficultyRate;
        State = GameState.Running;
        UpdateScore(0);
        _restartGameButton.onClick.AddListener(RestartGame);
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = $"Score: {_score}";
    }

    public void GameOver()
    {
        State = GameState.Over;
        _gameOverText.gameObject.SetActive(true);
        _restartGameButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
