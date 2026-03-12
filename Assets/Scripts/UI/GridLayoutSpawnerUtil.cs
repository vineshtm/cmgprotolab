using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutSpawnerUtil : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CardPrefab; //Card Prefab

    [SerializeField]
    private GridLayoutGroup m_CardGridLayout;

    [SerializeField]
    private RectTransform m_GridRectTransform;

    public List<GameObject> GenerateShuffledGrid(int rows, int columns)
    {
        //Instantiate Card Prefabs in Grid
        List<GameObject>  CardList = GenerateGrid(rows, columns);

        //Shuffle Card Indexes
        Util.Shuffle(CardList);

        return CardList;
    }

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

    private void ClearGridLayout()
    {
        for (int i = m_GridRectTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_GridRectTransform.GetChild(i).gameObject);
        }
    }
}
