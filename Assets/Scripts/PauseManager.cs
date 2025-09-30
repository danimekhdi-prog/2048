using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button PauseGameButton;
    public Button ClosePausePanelButton;
    public GameObject PausePanel;

    private RectTransform _pausePanelTransform;

    private void Awake()
    {
        ScreenInformation.OnEndScreenActivated += DeactivatePauseButton;

        _pausePanelTransform = PausePanel.GetComponent<RectTransform>();
        PausePanel.SetActive(false);
        PauseGameButton.onClick.AddListener(ActivatePausePanel);
        ClosePausePanelButton.onClick.AddListener(DeactivatePausePanel);
    }

    private void ActivatePausePanel()
    {
        _pausePanelTransform.anchoredPosition = new Vector3(0, 1600);
        PausePanel.SetActive(true);
        PauseGameButton.interactable = false;
        _pausePanelTransform.DOAnchorPos(Vector3.zero, 1f).OnComplete(() => Time.timeScale = 0f);
    }

    private void DeactivatePausePanel()
    {
        Time.timeScale = 1f;
        _pausePanelTransform.DOAnchorPos(new Vector3(0, 1600), 1f).OnComplete(() =>
        {
            PausePanel.SetActive(false);
            PauseGameButton.interactable = true;
        });
    }

    private void DeactivatePauseButton()
    {
        ScreenInformation.OnEndScreenActivated -= DeactivatePauseButton;

        PauseGameButton.interactable = false;        
    }
}
