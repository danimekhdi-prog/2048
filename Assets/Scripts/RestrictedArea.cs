using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    public static int Countdown = 0;
    [SerializeField] private TMP_Text InfoText;

    [SerializeField] private int _countdownToLoss = 3;

    private GameObject _newCube;

    private IEnumerator GameOverTimer()
    {
        
        Countdown = _countdownToLoss;
        for (int i = Countdown; i >= 0; i--)
        {
            InfoText.transform.DOScale(0.9f, 0.5f).OnComplete(() => InfoText.transform.localScale = Vector3.one);
            InfoText.text = $"Time to lose: {i:D3}";

            yield return new WaitForSeconds(1f);
            
        }
        GameManager.IsGameOver = true;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewCube"))
        {
            _newCube = other.gameObject;

            StartCoroutine(GameOverTimer());
        }

        if (other.CompareTag("SimpleCube"))
        {
            if (Countdown == 0)
            {
                StartCoroutine(GameOverTimer());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NewCube"))
        {
            InfoText.transform.localScale = Vector3.one;
            other.tag = "SimpleCube";
            _newCube = null;
            InfoText.text = "";
            StopAllCoroutines();
        }
        else if (other.CompareTag("SimpleCube"))
        {
            
            StopAllCoroutines();
            Countdown = 0;
        }
    }
}
