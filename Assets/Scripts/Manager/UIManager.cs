using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager m_GameManager;    

    // Start is called before the first frame update
    void Start()
    {
        //Load Gameplay screen
        m_GameManager.StartGame();
    }
}
