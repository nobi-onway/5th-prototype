using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField]
    private int _difficulty;
    private Button _button;
    private GameManager _gameManager;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(_difficulty);
    }
}
