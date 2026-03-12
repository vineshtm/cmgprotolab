using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GridCardView : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CardFront;

    [SerializeField]
    private GameObject m_CardBack;

    [SerializeField]
    private TextMeshProUGUI m_CardText;

    [SerializeField]
    private Button m_CardButton;

    public Action<GridCardView> OnCardClicked;
    public bool IsFlipped { get; private set; }
    public bool IsMatched { get; private set; }

    private Card m_Card;
    public Card Card { get { return m_Card; } }

    public void SetupCardView(Card CardData)
    {
        //Set Card
        m_Card = CardData;
        m_CardText.text = m_Card.CardName;

        ShowBack();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CardButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (IsFlipped || IsMatched)
            return;

        FlipFront();
        OnCardClicked?.Invoke(this);
    }

    public void FlipFront()
    {
        IsFlipped = true;

        m_CardFront.SetActive(true);
        m_CardBack.SetActive(false);
    }

    public void FlipBack()
    {
        IsFlipped = false;

        m_CardFront.SetActive(false);
        m_CardBack.SetActive(true);
    }

    public void Match()
    {
        IsMatched = true;

        //disable card
        gameObject.SetActive(false);
    }

    public void ShowBack()
    {
        m_CardFront.SetActive(false);
        m_CardBack.SetActive(true);

        IsFlipped = false;
    }
}
