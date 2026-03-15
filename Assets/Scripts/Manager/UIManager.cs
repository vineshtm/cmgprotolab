using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages UI
/// UI Interractions Interface
/// </summary>
public class UIManager : MonoBehaviour
{
    //MANAGER INSTANCE
    [Header("Manager Instance")]
    [SerializeField]
    private GameManager m_GameManager;

    //SCREENS
    [Header("Screens")]
    [SerializeField]
    private GameObject m_HomeScreen;

    [SerializeField]
    private GameObject m_GameplayScreen;

    [SerializeField]
    private GameObject m_GamePauseScreen;

    [SerializeField]
    private GameObject m_GameResultScreen;

    //HOME SCREEN UI
    [Header("Home Screen UI")]
    [SerializeField]
    private TMP_Dropdown m_LevelSelectDropDown;

    [SerializeField]
    private Button m_StartGameButton;

    //GAMEPLAY SCREEN UI
    [Header("Gameplay Screen UI")]
    [SerializeField]
    private Button m_PauseButton;

    //PAUSE SCREEN UI
    [Header("Pause Screen UI")]
    [SerializeField]
    private Button m_PauseHomeButton;

    [SerializeField]
    private Button m_PauseResumeButton;

    [SerializeField]
    private Button m_PauseRestartButton;

    //RESULT SCREEN UI
    [Header("Result Screen UI")]
    [SerializeField]
    private Button m_RestartGameButton;

    [SerializeField]
    private Button m_BackToHomeButton;

    [SerializeField]
    private TextMeshProUGUI m_Score;

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        //Subscribe for Events
        EventManager.OnUpdateGameData += UpdateScoreboard;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        //Unsibscribe for Evenets
        EventManager.OnUpdateGameData -= UpdateScoreboard;
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        //Add Home Screen UI Listeners
        m_StartGameButton.onClick.AddListener(OnStartGame);

        //Add Gameplay Screen UI Listeners
        m_PauseButton.onClick.AddListener(OnPauseGame);

        //Add Pause Screen UI Listeners
        m_PauseHomeButton.onClick.AddListener(ShowHomeScreen);
        m_PauseResumeButton.onClick.AddListener(OnResumeGame);
        m_PauseRestartButton.onClick.AddListener(OnRestartGame);

        //Add Result Screen UI Listeners
        m_RestartGameButton.onClick.AddListener(OnStartGame);
        m_BackToHomeButton.onClick.AddListener(ShowHomeScreen);
    }

    /// <summary>
    /// Show Scoreboard
    /// </summary>
    /// <param name="CurrentSessionData"></param>
    private void UpdateScoreboard(GameData CurrentSessionData)
    {
        m_Score.text = CurrentSessionData.Score.ToString();

        ShowResultScreen();
    }

    /// <summary>
    /// Start the Game
    /// Also used to Restart the Game on Game End
    /// </summary>
    private void OnStartGame()
    {
        ShowGameplayScreen(); //Screen Setup

        m_GameManager.StartGame(m_LevelSelectDropDown.value); //Init Game Manager to Start the Game
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    private void OnPauseGame()
    {
        ShowPauseScreen();

        m_GameManager.PauseGame();
    }

    /// <summary>
    /// Start the Game - from Pause Screen
    /// </summary>
    private void OnRestartGame()
    {
        ShowGameplayScreen(); //Screen Setup

        m_GameManager.RestartGame();
    }

    /// <summary>
    /// Resume Game - From Pause Screen 
    /// </summary>
    private void OnResumeGame()
    {
        ShowGameplayScreen();

        m_GameManager.ResumeGame();
    }

    /// <summary>
    /// Show Home Screen
    /// Enable Relevant Screen Gameobjects
    /// Disable irre;avant Gameobjects
    /// </summary>
    private void ShowHomeScreen()
    {
        m_HomeScreen.SetActive(true);
        m_GameplayScreen.SetActive(false);
        m_GamePauseScreen.SetActive(false);
        m_GameResultScreen.SetActive(false);
    }

    /// <summary>
    /// Show Game play
    /// Enable Relevant Screen Gameobjects
    /// Disable irre;avant Gameobjects
    /// </summary>
    private void ShowGameplayScreen()
    {
        m_HomeScreen.SetActive(false);
        m_GameplayScreen.SetActive(true);
        m_GamePauseScreen.SetActive(false);
        m_GameResultScreen.SetActive(false);
    }

    /// <summary>
    /// Show Game End Result Scree
    /// Enable Relevant Screen Gameobjects
    /// Disable irre;avant Gameobjects
    /// </summary>
    private void ShowResultScreen()
    {
        m_HomeScreen.SetActive(false);
        m_GameplayScreen.SetActive(false);
        m_GamePauseScreen.SetActive(false);
        m_GameResultScreen.SetActive(true);
    }

    /// <summary>
    /// Show Game Pause Screen
    /// Enable Relevant Screen Gameobjects
    /// Disable irre;avant Gameobjects
    /// </summary>
    private void ShowPauseScreen()
    {
        m_HomeScreen.SetActive(false);
        m_GameplayScreen.SetActive(false);
        m_GamePauseScreen.SetActive(true);
        m_GameResultScreen.SetActive(false);
    }
}
