using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Card Grid Spanwer Util
/// Based on the Number of Rows and Columns, Setup the Grid in the Rect/Area
/// Automatcally set Card sizes based on Available Rect/Area
/// </summary>
public class GridLayoutSpawnerUtil : MonoBehaviour
{
    /// <summary>
    /// Card View Entity Prefab
    /// </summary>
    [SerializeField]
    private GameObject m_CardPrefab; //Card Prefab

    /// <summary>
    /// Card Grid Layout Group
    /// </summary>
    [SerializeField]
    private GridLayoutGroup m_CardGridLayout;

    /// <summary>
    /// Card Grid Holder Cotainer Rect
    /// </summary>
    [SerializeField]
    private RectTransform m_GridRectTransform;

    /// <summary>
    /// Create Shuffled Grid
    /// Shuffle the Grid from 'GenerateGrid()' with Shuffle Util
    /// </summary>
    /// <param name="rows">No OF Rows</param>
    /// <param name="columns">No Of COlumns</param>
    /// <returns></returns>
    public List<GameObject> GenerateShuffledGrid(int rows, int columns)
    {
        //Instantiate Card Prefabs in Grid
        List<GameObject>  CardList = GenerateGrid(rows, columns);

        //Shuffle Card Indexes
        Util.Shuffle(CardList);

        return CardList;
    }

    /// <summary>
    /// Create the Grid
    /// Based on the no of rows and columns, create the grid
    /// Define the Card size based on the Available ARea
    /// Spawn/Instatiate the card prefab in the area/Rect Container
    /// Can also integrate Card prefab pooling instead of instantiating everytime.
    /// </summary>
    /// <param name="rows">No of Rows</param>
    /// <param name="columns">No of Columns</param>
    /// <returns></returns>
    public List<GameObject> GenerateGrid(int rows, int columns)
    {
        ClearGridLayout();

        float cardWidth = m_GridRectTransform.rect.width / columns;
        float cardHeight = m_GridRectTransform.rect.height / rows;

        float size = Mathf.Min(cardWidth - m_CardGridLayout.spacing.x, cardHeight - m_CardGridLayout.spacing.y);

        m_CardGridLayout.cellSize = new Vector2(size, size);
        m_CardGridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        m_CardGridLayout.constraintCount = columns;

        int totalCards = rows * columns;

        List<GameObject> cards = new List<GameObject>();

        for (int i = 0; i < totalCards; i++)
        {
            GameObject card = Instantiate(m_CardPrefab, m_GridRectTransform);

            cards.Add(card);
        }

        return cards;
    }

    /// <summary>
    /// Clear Exisiting Grid Layout to reset the Grid
    /// Not Required when Pool Cards. Cards can be reused instead of destroying it
    /// </summary>
    private void ClearGridLayout()
    {
        for (int i = m_GridRectTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_GridRectTransform.GetChild(i).gameObject);
        }
    }
}
