using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ScoringMechanism m_ScoringMechanism;

    [SerializeField]
    private GridLayoutSpawnerUtil m_GridSpawner;

    [SerializeField]
    private int Rows = 2; //Temporarily serialize it

    [SerializeField]
    private int Columns = 2; //Temporarily serialize it

    private List<GameObject> m_CurrentSessionCardList;
    private List<GridCardView> m_SelectedCardList = new List<GridCardView>();

    private int remainingPairs;

    //Game Events Declarations  
    public Action<bool> OnStartGame;
    public Action<int> OnEndGame;
    public Action<bool> OnCardSelected;
    public Action<bool> OnCardMatchingChecked;

    public void StartGame()
    {
        SetupGrid(Rows, Columns);

        remainingPairs = (Rows * Columns) / 2;

        m_ScoringMechanism.ResetScore();

        OnStartGame?.Invoke(true);
    }

    public void SetGridRowsAndColumns(int LevelIndex)
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

    //GRID SETUP
    private void SetupGrid(int GridRows, int GridColumns)
    {
        //Instantiate Card Prefabs in Grid
        m_CurrentSessionCardList = m_GridSpawner.GenerateShuffledGrid(GridRows, GridColumns);

        //Setup Cards
        List<Card> cardList = GenerateCards(m_CurrentSessionCardList.Count / 2);

        for (int i = 0; i < m_CurrentSessionCardList.Count; i++)
        {
            GridCardView cardview = m_CurrentSessionCardList[i].GetComponent<GridCardView>();
            cardview.SetupCardView(cardList[i % (cardList.Count)]);
            cardview.OnCardClicked += OnCardClicked;
        }
    }

    void OnCardClicked(GridCardView card)
    {
        OnCardSelected?.Invoke(true);
        HandleCardClick(card);
    }    

    public void HandleCardClick(GridCardView card)
    {
        m_SelectedCardList.Add(card);

        if (m_SelectedCardList.Count == 2)
        {
            StartCoroutine(CheckCardMatching());
        }
    }

    IEnumerator CheckCardMatching()
    {
        GridCardView SelectedCardOne = m_SelectedCardList[0];
        GridCardView SelectedCardTwo = m_SelectedCardList[1];

        m_SelectedCardList.Clear();

        yield return new WaitForSeconds(0.5f);

        if (SelectedCardOne.Card.CardId == SelectedCardTwo.Card.CardId)
        {
            SelectedCardOne.Match();
            SelectedCardTwo.Match();

            m_ScoringMechanism.AddScore(10);

            OnPairMatched();

            OnCardMatchingChecked?.Invoke(true);
        }
        else
        {
            SelectedCardOne.FlipBack();
            SelectedCardTwo.FlipBack();

            m_ScoringMechanism.AddScore(-5);

            OnCardMatchingChecked?.Invoke(false);
        }
    }

    void OnPairMatched()
    {
        remainingPairs--;

        if (remainingPairs <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        int finalScore = m_ScoringMechanism.Score;
        Debug.Log("============" + finalScore);

        OnEndGame?.Invoke(finalScore);
    }

    //UTIL
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
}
