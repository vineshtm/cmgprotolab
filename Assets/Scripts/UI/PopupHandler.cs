using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Popup Handler
/// Component to dynamically set Popup Content and Action Handlers
/// Title
/// Message
/// Left Button
/// Right Button
/// </summary>
public class PopupHandler : MonoBehaviour
{
    [Header("Content Text")]
    [SerializeField]
    private TextMeshProUGUI m_PopupTitleText;

    [SerializeField]
    private TextMeshProUGUI m_PopupContentInfoText;

    [Header("Confirmation UI")]
    [SerializeField]
    private Button m_LeftButton;

    [SerializeField]
    private TextMeshProUGUI m_LeftButtonText;
    
    [SerializeField]
    private Button m_RightButton;

    [SerializeField]
    private TextMeshProUGUI m_RightButtonText;

    /// <summary>
    /// Set Popup Details
    /// Set the Title for the Popup Panel
    /// Set the Content Text for the Popup panel
    /// Left Button Handles - Set the Text for Left Button
    /// Left Button Handles - Set the Action for Left Button
    /// Right Button Handles - Set the Text for Right Button
    /// Right Button Handles - Set the Action for Right Button
    /// </summary>
    /// <param name="Title"></param>
    /// <param name="Content"></param>
    /// <param name="BtnLeftText"></param>
    /// <param name="LeftButtonAction"></param>
    /// <param name="BtnRightText"></param>
    /// <param name="RightButtonAction"></param>
    public void SetPopUp(string Title, string Content,
        string BtnLeftText, UnityAction LeftButtonAction,
        string BtnRightText, UnityAction RightButtonAction)
    {
        //Title Text
        m_PopupTitleText.text = Title;

        //Content Info Text
        m_PopupContentInfoText.text = Content;

        //Left Button Handles
        m_LeftButtonText.text = BtnLeftText;

        m_LeftButton.onClick.RemoveAllListeners();
        if (LeftButtonAction != null)
            m_LeftButton.onClick.AddListener(LeftButtonAction);

        //Right Button Handles
        m_RightButtonText.text = BtnRightText;

        m_RightButton.onClick.RemoveAllListeners();
        if (RightButtonAction != null)
            m_RightButton.onClick.AddListener(RightButtonAction);

    }

    /// <summary>
    /// Close Popup Panel
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
