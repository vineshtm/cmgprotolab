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

    [SerializeField]
    private Timer m_Timer;

    [SerializeField]
    private PopupHandler m_PopupHandler;

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
    /// <summary>
    /// Pause Button
    /// </summary>
    [SerializeField]
    private Button m_PauseButton;

    /// <summary>
    /// Time Text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    //PAUSE SCREEN UI
    [Header("Pause Screen UI")]
    [SerializeField]
    private Button m_PauseHomeButton;

    /// <summary>
    /// Resume Button from Pause Screen
    /// </summary>
    [SerializeField]
    private Button m_PauseResumeButton;

    /// <summary>
    /// Restart Button from Pause Screen
    /// </summary>
    [SerializeField]
    private Button m_PauseRestartButton;

    //RESULT SCREEN UI
    [Header("Result Screen UI")]
    [SerializeField]
    private Button m_RestartGameButton;

    [SerializeField]
    private Button m_BackToHomeButton;

    [SerializeField]
    private TextMeshProUGUI m_LevelText;

    [SerializeField]
    private TextMeshProUGUI m_TimeText;

    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    //GAME DATA MANAGEMENT
    [Header("Game Data Management")]
    [SerializeField]
    private GameObject m_GameDataScreen;

    [SerializeField]
    private Button m_DisplayGameDataButton;

    [SerializeField]
    private TextMeshProUGUI m_GameDataText;

    [SerializeField]
    private Button m_ClearGameProgressDataButton;

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        //Subscribe for Game Events
        EventManager.OnUpdateGameData += UpdateScoreboard;

        //Subscribe for Timer Tick
        m_Timer.OnSecondTick += UpdateTimer;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        //Unsibscribe for Game Evenets
        EventManager.OnUpdateGameData -= UpdateScoreboard;

        //UnSubscribe for Timer Tick
        m_Timer.OnSecondTick -= UpdateTimer;
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

        //Game Data
        m_DisplayGameDataButton.onClick.AddListener(OnLoadGameData);
        m_ClearGameProgressDataButton.onClick.AddListener(ClearGameProgressData);
    }

    /// <summary>
    /// Show Scoreboard
    /// </summary>
    /// <param name="CurrentSessionData"></param>
    private void UpdateScoreboard(GameData CurrentSessionData)
    {
        m_LevelText.text = "Level : " + ((DifficultyLevel)CurrentSessionData.DifficultyLevelIndex);
        m_TimeText.text = "Time : " + CurrentSessionData.Time;
        m_ScoreText.text = "Score : " + CurrentSessionData.Score;

        ShowResultScreen();
    }

    private void UpdateTimer(int Time)
    {
        m_TimerText.text = m_Timer.ElapsedTimeFormatted;
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
    /// Display the Progress Data
    /// </summary>
    private void OnLoadGameData()
    {
        ////Display Entire Game Data List as a Single Text
        //string GameDataLog = m_GameManager.LogProgressData();
        //m_GameDataText.text = GameDataLog;

        //Load Game Data and Display it as List in UI
        m_GameManager.LoadProgressData();

        m_GameDataScreen.SetActive(true);
    }

    /// <summary>
    /// Clear the Game ProgressData
    /// </summary>
    private void ClearGameProgressData()
    {
        m_PopupHandler.gameObject.SetActive(true);
        m_PopupHandler.SetPopUp("Warning", "Clear All progress Data?",
            "NO", () =>
            {
                m_PopupHandler.gameObject.SetActive(false);
            },
            "YES", () =>
             {
                 m_GameManager.ClearGameProgressData();
                 m_PopupHandler.gameObject.SetActive(false);
             });
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
