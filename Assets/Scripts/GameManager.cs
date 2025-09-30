using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public static Action OnRecordScoreUpdated;
    public static int CurrentGameScore;
    public static int ReccordGameScore;
    public static int HighestNumberOnCube;

    public Button RestartGameButton;

    public static bool IsGameOver;
    

    private void Awake()
    {
        RestartGameButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));

        IsGameOver = false;
        CurrentGameScore = 0;
        HighestNumberOnCube = 0;

        CubeController[] cubes = FindObjectsOfType<CubeController>();
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i].CubeNumber > HighestNumberOnCube)
            {
                HighestNumberOnCube = cubes[i].CubeNumber;
            }
        }
    }

    public static void UpdateScore(int score)
    {
        CurrentGameScore += score;

        if (ReccordGameScore < CurrentGameScore)
        {
            OnRecordScoreUpdated?.Invoke();

            ReccordGameScore = CurrentGameScore;
        }
    }
}
