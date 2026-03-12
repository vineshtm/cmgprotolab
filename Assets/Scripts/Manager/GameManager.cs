using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GridLayoutSpawnerUtil m_GridSpawner;

    [SerializeField]
    private int Rows = 2; //Temporarily serialize it

    [SerializeField]
    private int Columns = 2; //Temporarily serialize it

    private List<GameObject> m_CurrentSessionCardList;
    private List<GridCardView> m_SelectedCardList = new List<GridCardView>();

    public void StartGame()
    {
        SetupGrid(Rows, Columns);
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
        }
        else
        {
            SelectedCardOne.FlipBack();
            SelectedCardTwo.FlipBack();
        }
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

            cardList.Add(card);
        }

        return cardList;
    }
}
