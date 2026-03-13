using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager m_GameManager;

    [SerializeField]
    private GameObject m_HomeScreen;

    [SerializeField]
    private GameObject m_GameplayScreen;

    [SerializeField]
    private GameObject m_GameResultScreen;

    //HOME SCREEN UI
    [SerializeField]
    private TMP_Dropdown m_LevelSelectDropDown;

    [SerializeField]
    private Button m_StartGameButton;

    //RESULT SCREEN UI
    [SerializeField]
    private Button m_RestartGameButton;

    [SerializeField]
    private Button m_BackToHomeButton;

    [SerializeField]
    private TextMeshProUGUI m_Score;

    private void OnEnable()
    {
        //m_GameManager.OnStartGame += OnStartGame;
        //m_GameManager.OnEndGame += OnEndGame;

        EventManager.OnGameStart += OnStartGame;
        EventManager.OnGameEnd += OnEndGame;

        EventManager.OnFinalScoreUpdate += UpdateScoreboard;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= OnStartGame;
        EventManager.OnGameEnd -= OnEndGame;

        EventManager.OnFinalScoreUpdate -= UpdateScoreboard;
    }

    private void OnStartGame()
    {
    }

    private void OnEndGame()
    {
        ShowResultScreen();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Add Listeners
        m_StartGameButton.onClick.AddListener(StartGame);

        m_RestartGameButton.onClick.AddListener(StartGame);

        m_BackToHomeButton.onClick.AddListener(ShowHomeScreen);

        m_LevelSelectDropDown.onValueChanged.AddListener(LevelSelected);
    }

    private void StartGame()
    {
        ShowGameplayScreen();

        //Load Gameplay screen
        m_GameManager.StartGame();
    }

    private void ShowHomeScreen()
    {
        m_HomeScreen.SetActive(true);
        m_GameplayScreen.SetActive(false);
        m_GameResultScreen.SetActive(false);
    }

    private void ShowGameplayScreen()
    {
        m_HomeScreen.SetActive(false);
        m_GameplayScreen.SetActive(true);
        m_GameResultScreen.SetActive(false);
    }

    private void ShowResultScreen()
    {
        m_HomeScreen.SetActive(false);
        m_GameplayScreen.SetActive(false);
        m_GameResultScreen.SetActive(true);
    }

    private void UpdateScoreboard(int Score)
    {
        m_Score.text = Score.ToString();
    }

    private void LevelSelected(int index)
    {
        m_GameManager.SetGridRowsAndColumns(index);
    }
}
