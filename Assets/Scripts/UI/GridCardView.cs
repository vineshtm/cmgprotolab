using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Card View in Grid
/// Attached to Root Prefab
/// </summary>
public class GridCardView : MonoBehaviour
{
    /// <summary>
    /// card front Gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_CardFront;

    /// <summary>
    /// Card back gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_CardBack;

    /// <summary>
    /// Card Text on the Front
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_CardText;

    /// <summary>
    /// Card Button
    /// </summary>
    [SerializeField]
    private Button m_CardButton;

    /// <summary>
    /// Animator - Manages Card Animations on Evenets
    /// </summary>
    [SerializeField]
    private Animator m_CardAnimator;

    /// <summary>
    /// On Card Click Event
    /// </summary>
    public Action<GridCardView> OnCardClicked;

    /// <summary>
    /// Card Current State - Is Flipped
    /// </summary>
    public bool IsFlipped { get; private set; }

    /// <summary>
    /// Card Current State - Is Matched
    /// </summary>
    public bool IsMatched { get; private set; }

    /// <summary>
    /// Card Data
    /// </summary>
    private Card m_Card;
    public Card Card { get { return m_Card; } }

    /// <summary>
    /// Setup Card Properties for the View
    /// </summary>
    /// <param name="CardData"></param>
    public void SetupCardView(Card CardData)
    {
        //Set Card
        m_Card = CardData;
        m_CardText.text = m_Card.CardName;
        m_CardFront.GetComponent<Image>().color = m_Card.CardFrontColor;

        ShowBack(); //By Default Flip back the card initialy
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CardButton.onClick.AddListener(OnClick); //Register Click
    }

    /// <summary>
    /// On CLick Event to handle card click
    /// </summary>
    public void OnClick()
    {
        if (IsFlipped || IsMatched)
            return;

        FlipFront();

        OnCardClicked?.Invoke(this);
    }

    /// <summary>
    /// Show Card Front
    /// With Animation
    /// </summary>
    public void FlipFront()
    {
        IsFlipped = true;

        //m_CardFront.SetActive(true);
        //m_CardBack.SetActive(false);

        m_CardAnimator?.SetTrigger("flipFront");
    }

    /// <summary>
    /// Show Card Back
    /// With Animation
    /// </summary>
    public void FlipBack()
    {
        IsFlipped = false;

        //m_CardFront.SetActive(false);
        //m_CardBack.SetActive(true);

        m_CardAnimator?.SetTrigger("flipBack");
    }

    /// <summary>
    /// handle Card Match
    /// </summary>
    public void Match()
    {
        IsMatched = true;

        //disable card
        //gameObject.SetActive(false);

        DisableCard();
    }

    /// <summary>
    /// Show Card Back - Reset
    /// </summary>
    public void ShowBack()
    {
        m_CardFront.SetActive(false);
        m_CardBack.SetActive(true);

        IsFlipped = false;
    }

    /// <summary>
    /// Disbale/Hide the Card
    /// With Animation
    /// </summary>
    private void DisableCard()
    {
        //m_CardFront.SetActive(false);
        //m_CardBack.SetActive(false);

        m_CardAnimator?.SetTrigger("Disable");
    }
}
