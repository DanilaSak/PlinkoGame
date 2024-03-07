using System.Collections;
using System.Collections.Generic;
using Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : SingletonMonoBehaviour<UIController>
{
    [Header("Screens")] [SerializeField] CanvasGroup mainScreen;
    [SerializeField] CanvasGroup gameScreen;
    [SerializeField] CanvasGroup exitScreen;
    [SerializeField] CanvasGroup soundScreen;

    [Header("Game Screen")] [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI totalKnockedText;
    [SerializeField] TextMeshProUGUI totalKnockedText2;
    [SerializeField] TextMeshProUGUI totalKnockedText3;

    [SerializeField] TextMeshProUGUI comboScoreText;

    [SerializeField] TextMeshProUGUI comboMultiplierText;

    [SerializeField] TextMeshProUGUI goalScoreText;
    [SerializeField] TextMeshProUGUI timeLeftText;
    [SerializeField] private TextMeshProUGUI timeLeftText2;

    [SerializeField] TextMeshProUGUI highscoreText;

    [SerializeField] Animator gameOverView;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    CanvasGroup currentScreen;

    float timePulse;

    public static void ShowMainScreen()
    {
        UpdateHighScore(GameController.highscore);
        instance.StartCoroutine(
            instance.IEChangeScreen(instance.mainScreen, executeAfter: () =>
            {
                //  GameController.ShowGemMenu();
            })
        );
    }

    public static void ShowExitScreen()
    {
        instance.StartCoroutine(
            instance.IEChangeScreen(instance.exitScreen, executeAfter: () =>
            {
                //  GameController.ShowGemMenu();
            })
        );
    }

    public static void ShowSettingScreen()
    {
        instance.StartCoroutine(
            instance.IEChangeScreen(instance.soundScreen, executeAfter: () =>
            {
                //  GameController.ShowGemMenu();
            })
        );
    }

    public static void OpenGameOverView(int score)
    {
        // instance.gameOverScoreText.text = $"{score}";
        // instance.gameOverView.SetTrigger("pulse");
    }

    public static void ShowGameScreen()
    {
        UpdateScore(GameController.score);
        UpdateGoalScore(GameController.currentGoalScore);
        UpdateTimeLeft(GameController.timeLeft);
        instance.StartCoroutine(
            instance.IEChangeScreen(instance.gameScreen, () =>
            {
                //GameController.ShowGemMenu(false);
            })
        );
    }

    public static void UpdateScore(int score)
    {
        instance.scoreText.text = $"{score}/{GameController.winScore}";
    }

    public static void UpdateTotalKnocked(int score)
    {
        instance.totalKnockedText.text = $"{score}";
        instance.totalKnockedText2.text = $"{score}";
        instance.totalKnockedText3.text = $"{score}";
    }

    public static void UpdateComboScore(int comboScore, int multiplier)
    {
        return;
        instance.comboScoreText.text = $"+{comboScore / Mathf.Max(multiplier, 1)}";
        instance.comboMultiplierText.text = multiplier > 1 ? $" x{multiplier}" : "";

        instance.comboScoreText.GetComponent<Animator>().SetTrigger("pulse");
    }


    public static void UpdateHighScore(int score)
    {
        // instance.highscoreText.text = $"High Score: {score}";
    }

    public static void UpdateGoalScore(int goalScore)
    {
        //   instance.goalScoreText.text = $"/{ goalScore }";
        //  instance.goalScoreText.GetComponent<Animator>().SetTrigger("pulse");
    }

    public static void UpdateTimeLeft(float timeLeft)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timeLeft);
        string mm = timeSpan.Minutes.ToString("D2");
        string ss = timeSpan.Seconds.ToString("D2");
        instance.timeLeftText.text = $"TIME: {mm}:{ss}";
        instance.timeLeftText2.text = $"TIME: {mm}:{ss}";
    }

    IEnumerator IEChangeScreen(
        CanvasGroup screen,
        System.Action executeBefore = null, System.Action executeAfter = null
    )
    {
        if (executeBefore != null)
            executeBefore();

        screen.alpha = 0;
        screen.gameObject.SetActive(false);

        if (currentScreen)
        {
            while (currentScreen.alpha > 0)
            {
                currentScreen.alpha -= Time.deltaTime * 2;
                yield return null;
            }

            currentScreen.gameObject.SetActive(false);
        }

        currentScreen = screen;
        currentScreen.gameObject.SetActive(true);

        while (currentScreen.alpha < 1)
        {
            currentScreen.alpha += Time.deltaTime * 2;
            yield return null;
        }

        if (executeAfter != null)
            executeAfter();
    }
}