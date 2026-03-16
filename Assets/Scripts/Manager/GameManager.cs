using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager
/// Manage the Game Flow
/// Interact with Scoring Mechanism to Update Scores
/// Interracts with Progress Manager to Update the Game Progress
/// </summary>
public class GameManager : MonoBehaviour
{
    //MANAGER INSTANCE
    [Header("Manager Instance")]
    [SerializeField]
    private ScoringMechanism m_ScoringMechanism;

    [SerializeField]
    private ProgressManager m_ProgressManager;

    [SerializeField]
    private Timer m_Timer;

    [SerializeField]
    private GridLayoutSpawnerUtil m_GridSpawner;

    [SerializeField]
    private ProgressDataDisplayUtil m_DisplayGameDataUtil;

    //PRIVATE VARIABLES
    private int Rows = 2;
    private int Columns = 2;

    /// <summary>
    /// List of Selected Cards - Manage Selected Cards
    /// </summary>
    private List<GridCardView> m_SelectedCardList = new List<GridCardView>();

    /// <summary>
    /// Remaining Pairs of Cards to Match
    /// </summary>
    private int remainingPairs;

    /// <summary>
    /// Deatils of the Currrent Game Session
    /// </summary>
    private GameData m_CurrentGameDetails;

    /// <summary>
    /// Start Game
    /// Inits the Game - Resets Game Parameters
    /// SetupCreate Card Grid
    /// Trigger Game Start Event to Notify
    /// </summary>
    /// <param name="LevelIndex">Difficulty Level Index to identofy the Grid Layout</param>
    public void StartGame(int LevelIndex)
    {
        CreateNewGameSession(LevelIndex);//Init Game

        SetupGrid(Rows, Columns);//Grid Layout Setup

        m_Timer.StartTimer(); //Start Timer

        EventManager.StartGame(); //trigger Game Session Ready
    }

    /// <summary>
    /// Pause Game
    /// </summary>
    public void PauseGame()
    {
        m_Timer.PauseTimer(); //Pause Timer
    }

    /// <summary>
    /// Restart Game from Pause
    /// </summary>
    public void RestartGame()
    {
        int CurrentLevelIndex = m_CurrentGameDetails.DifficultyLevelIndex;

        StartGame(CurrentLevelIndex);
    }

    /// <summary>
    /// Resume Game from Pause
    /// </summary>
    public void ResumeGame()
    {
        m_Timer.ResumeTimer(); //Resume Timer
    }

    /// <summary>
    /// Create a New Game session Instance
    /// </summary>
    /// <param name="LevelIndex"></param>
    private void CreateNewGameSession(int LevelIndex)
    {
        //Set the Rows and Columns for the Grid based on The Selected Difficulty Level
        SetGridRowsAndColumns(LevelIndex);

        //Clear Selected Card Set
        m_SelectedCardList?.Clear();

        //Reset Game Parameters
        remainingPairs = (Rows * Columns) / 2;

        //Reset Current Game Session data
        if (m_CurrentGameDetails == null)
            m_CurrentGameDetails = new GameData();

        m_CurrentGameDetails.DifficultyLevelIndex = LevelIndex;
        m_CurrentGameDetails.Attempts = 0;

        //Reset Score
        m_ScoringMechanism.ResetScoring();
    }

    /// <summary>
    /// Setup Grid
    /// Create the Grid Layout
    /// Instantiate the Cards Prefabs to scripts
    /// Setup the View and Card Deatils
    /// </summary>
    /// <param name="GridRows"></param>
    /// <param name="GridColumns"></param>
    private void SetupGrid(int GridRows, int GridColumns)
    {
        //Instantiate Card Prefabs in Grid
        List<GameObject> CurrentSessionCardList = m_GridSpawner.GenerateShuffledGrid(GridRows, GridColumns);

        //Setup Cards
        List<Card> cardList = GenerateCards(CurrentSessionCardList.Count / 2);

        for (int i = 0; i < CurrentSessionCardList.Count; i++)
        {
            GridCardView cardview = CurrentSessionCardList[i].GetComponent<GridCardView>();
            cardview.SetupCardView(cardList[i % (cardList.Count)]);
            cardview.OnCardClicked += OnCardClicked;
        }
    }

    /// <summary>
    /// On Card Click Event
    /// </summary>
    /// <param name="card"></param>
    void OnCardClicked(GridCardView card)
    {
        EventManager.CardSelected(); //Trigger Card Clicked Event

        HandleCardClick(card); //Handle click
    }

    /// <summary>
    /// Hanbdle Click on Card
    /// </summary>
    /// <param name="card"></param>
    public void HandleCardClick(GridCardView card)
    {
        m_SelectedCardList.Add(card); //Register selection of Card

        if (m_SelectedCardList.Count == 2) //Check if 2 cards are selected.
        {
            StartCoroutine(CheckCardMatching()); //Run Matching Logic If 2 Cards selected
        }
    }

    /// <summary>
    /// Check if Selected Cards Match
    /// Manage the Attempts
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckCardMatching()
    {
        m_CurrentGameDetails.Attempts++;//Increment Attempts for current game session

        GridCardView SelectedCardOne = m_SelectedCardList[0]; //first clicked/seected card for matching
        GridCardView SelectedCardTwo = m_SelectedCardList[1]; //second clicked/selected card for matching

        m_SelectedCardList.Clear(); //Clear List to manage next clicks before matching logic

        yield return new WaitForSeconds(0.5f);

        if (SelectedCardOne.Card.CardId == SelectedCardTwo.Card.CardId)
        {
            SelectedCardOne.Match(); //Update Card State
            SelectedCardTwo.Match();  //Update Card State

            m_ScoringMechanism.ReportMatchResult(true); // Report Match to Scoring Manager

            OnPairMatched();

            EventManager.CardMatchResult(true);
        }
        else
        {
            SelectedCardOne.FlipBack(); //Update Card State
            SelectedCardTwo.FlipBack(); //Update Card State

            m_ScoringMechanism.ReportMatchResult(false); // Report Match to Scoring Manager

            EventManager.CardMatchResult(false);
        }
    }

    /// <summary>
    /// Handle Pair Match
    /// And identify all pairs are matched
    /// </summary>
    private void OnPairMatched()
    {
        remainingPairs--;

        if (remainingPairs <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Game Over
    /// Update the Scores
    /// Generate Current Game Session Data
    /// Declare Result
    /// Save the Progress
    /// </summary>
    private void GameOver()
    {
        m_Timer.StopTimer();

        m_CurrentGameDetails.Score = m_ScoringMechanism.Score; //Update Game Score
        m_CurrentGameDetails.Time = m_Timer.ElapsedTimeFormatted; //Update the Game Time

        EventManager.EndGame();
        EventManager.DeclareGameData(m_CurrentGameDetails); //Trigger Game Over/Result Declare

        //Save Game Data
        m_ProgressManager.SaveCurrentGameData(m_CurrentGameDetails);

#if UNITY_EDITOR
        //LOG for Debug Purpose
        Debug.Log("========GAME OVER=========="
            + "====" + m_CurrentGameDetails.DifficultyLevelIndex
            + "====" + m_CurrentGameDetails.Score
            + "====" + m_CurrentGameDetails.Attempts
            + "====" + m_CurrentGameDetails.Time);
#endif
    }

    //UTILS (TEMPORARY)

    /// <summary>
    /// Set the Grid Rows and COlumns based on The selected Difficulty Level Index
    /// Hard Coded now
    /// </summary>
    /// <param name="LevelIndex"></param>
    private void SetGridRowsAndColumns(int LevelIndex)
    {
        switch (LevelIndex)
        {
            case 0: { Rows = 2; Columns = 2; break; } //Beginner
            case 1: { Rows = 3; Columns = 2; break; } //Medium
            case 2: { Rows = 4; Columns = 3; break; } //Hard
            case 3: { Rows = 5; Columns = 4; break; } //Expert
            case 4: { Rows = 6; Columns = 5; break; } //Nightmare
        }
    }

    /// <summary>
    /// Creates Sets Of Cards
    /// Card properties Generated randomly
    /// </summary>
    /// <param name="CardCount"></param>
    /// <returns></returns>
    private List<Card> GenerateCards(int CardCount)
    {
        List<Card> cardList = new List<Card>();
        for (int i = 0; i < CardCount; i++)
        {
            Card card = new Card();
            card.CardId = i.ToString();
            card.CardName = i.ToString();
            card.CardFrontColor = UnityEngine.Random.ColorHSV();

            cardList.Add(card);
        }

        return cardList;
    }

    //PUBLIC UTIL

    /// <summary>
    /// Get the Game Progress data and Display in UI as Table with Display Util
    /// Get the Progress Data and Display in UI
    /// </summary>
    public void LoadProgressData()
    {
        ProgressData CurrentProgressData = m_ProgressManager.LoadProgress();

        if (CurrentProgressData != null)
        {
            m_DisplayGameDataUtil.SetTotalGameSessionText(CurrentProgressData.TotalGameSessions);
            m_DisplayGameDataUtil.SetHighscoreText(CurrentProgressData.HighestScore);

            List<GameData> GameDataList = CurrentProgressData.GameDetailList;

            //Instantiate Game Data Item Prefabs in Grid
            List<GameObject> CurrentSessionCardList = m_DisplayGameDataUtil.GenerateListView(GameDataList.Count);

            for (int i = 0; i < GameDataList.Count; i++)
            {
                GameDataItemUI cardview = CurrentSessionCardList[i].GetComponent<GameDataItemUI>();
                cardview.SetGameData((i + 1), GameDataList[i]);
            }
        }
        else
        {
            m_DisplayGameDataUtil.SetNoData();
        }
    }

    /// <summary>
    /// Clear the Game Progress data
    /// </summary>
    public void ClearGameProgressData()
    {
        m_ProgressManager.ClearAllProgressData();
    }

    /// <summary>
    /// Util to Create a String of Complete Progress Data
    /// </summary>
    /// <returns></returns>
    public string LogProgressData()
    {
        string ProgressDataString = "";

        ProgressData CurrentProgressData = m_ProgressManager.LoadProgress();

        if (CurrentProgressData != null)
        {
            List<GameData> GameDataList = CurrentProgressData.GameDetailList;

            string GameDataUnitString = "";

#if UNITY_EDITOR
            string EditorLogDataString = "";
#endif

            for (int i = 0; i < GameDataList.Count; i++)
            {
                GameDataUnitString += (i+1) + "."+
                    " Level:" + ((DifficultyLevel)GameDataList[i]?.DifficultyLevelIndex) +
                    ", Time:" + GameDataList[i]?.Time +
                    ", Score:" + GameDataList[i]?.Score +
                    " \n";

#if UNITY_EDITOR
                EditorLogDataString += (i + 1) + "." +
                    " Level:" + ((DifficultyLevel)GameDataList[i]?.DifficultyLevelIndex) +
                    " Time:" + GameDataList[i]?.Time +
                    " Score:" + GameDataList[i]?.Score +
                    " Attempts:" + GameDataList[i]?.Attempts +
                    " \n";
#endif
            }

            ProgressDataString = ("\n"
                + "Total Game Sessions : " + CurrentProgressData.TotalGameSessions + "\n"
                + "Highest Score : " + CurrentProgressData.HighestScore + "\n\n"
                + GameDataUnitString + "\n");

#if UNITY_EDITOR
            Debug.Log("==========PROGRESS DATA==============" + "\n"
                + "Total Game Sessions : " + CurrentProgressData.TotalGameSessions + "\n"
                + "Highest Score : " + CurrentProgressData.HighestScore + "\n"
                + EditorLogDataString + "\n" +
                "========================================");
#endif
        }
        else
        {
            ProgressDataString = "         NO PROGRESSDATA         ";
            Debug.Log("======NO PROGRESSDATA===========");
        }

        return ProgressDataString;
    }
}
