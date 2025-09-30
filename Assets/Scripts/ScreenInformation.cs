using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class ScreenInformation : MonoBehaviour
{
    public static event Action OnEndScreenActivated;

    [SerializeField] private TMP_Text CurrentGameScoreInfo;
    [SerializeField] private TMP_Text RecordGameScoreInfo;
    [SerializeField] private TMP_Text HighestNumberOnCubeInfo;
    
    [SerializeField] private GameObject GameOverWindow;

    private CanvasGroup _gameOverWindowAlpha;
    private RectTransform _gameOverWindowTransform;

    private void Awake()
    {
        Time.timeScale = 1f;
        _gameOverWindowAlpha = GameOverWindow.GetComponent<CanvasGroup>();
        _gameOverWindowTransform = GameOverWindow.GetComponent<RectTransform>();

        GameManager.OnRecordScoreUpdated += ScaleUpRecordText;
    }

    private void FixedUpdate()
    {
        CurrentGameScoreInfo.text = $"Score: {GameManager.CurrentGameScore}";
        RecordGameScoreInfo.text = $"Record: {GameManager.ReccordGameScore}";
        HighestNumberOnCubeInfo.text = $"HighestCubeNumber: {GameManager.HighestNumberOnCube}";
       
        if (GameManager.IsGameOver && GameOverWindow.activeSelf == false)
        {
            ScaleUpAndAppearGameOverPanel();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnRecordScoreUpdated -= ScaleUpRecordText;
    }

    private void ScaleUpAndAppearGameOverPanel()
    {
        OnEndScreenActivated?.Invoke();
        GameOverWindow.SetActive(true);
        _gameOverWindowTransform.localScale = Vector3.zero;
        _gameOverWindowAlpha.DOFade(1, 1.5f);
        _gameOverWindowTransform.DOScale(1, 1.5f);
    }

    private void ScaleUpRecordText()
    {
        RecordGameScoreInfo.transform.DOScale(0.8f, 0.5f).OnComplete(() => RecordGameScoreInfo.transform.localScale = Vector3.one);
    }
}
